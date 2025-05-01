using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;


namespace HardwareShop.Controllers
{
    [Authorize(Roles = "Client")]
    public class ComandaController : Controller
    {
        private readonly ApplicationDbContext context;

        public ComandaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Afișează pagina inițială de comandă, cu produsele din coș și prețul total
        public async Task<ActionResult> DetaliiComanda(float pret)
        {
            var userId = User.Identity.GetUserId();

            // Se încarcă produsele din coșul utilizatorului
            List<CosCumparaturi> listaProduse = await context.cos
                .Include(c => c.Produs)
                .Where(c => c.ClientId == userId)
                .ToListAsync();

            // Se stochează în sesiune lista produselor pentru utilizare ulterioară
            Session["produseCos"] = listaProduse;

            // Se inițializează comanda cu prețul total
            Comanda comanda = new Comanda { PretComanda = pret };

            var viewModel = new DetaliiComandaViewModel()
            {
                comanda = comanda,
                etapa = "Facturare" // prima etapă a comenzii
            };

            return View(viewModel);
        }

        // Preia datele din formularul de facturare/plată și le validează
        [HttpPost]
        public ActionResult DetaliiComanda(string etapa, Comanda comanda, DetaliiComandaViewModel detaliiComanda, string eroare)
        {
            // Se verifică validitatea datelor și dacă există erori de plată
            if (!ModelState.IsValid || detaliiComanda.comanda == null || eroare != null)
            {
                detaliiComanda.comanda = comanda;
                detaliiComanda.card = new Card(); // reinitializăm cardul pentru view
                detaliiComanda.etapa = etapa;
                return View(detaliiComanda); // se revine în view cu erorile
            }

            // Se serializează datele comenzii și se salvează temporar
            var serializedData = JsonConvert.SerializeObject(detaliiComanda);
            TempData["detaliiComanda"] = serializedData;

            return RedirectToAction("Salvare", "Comanda");
        }

        // Salvează efectiv comanda în baza de date
        public async Task<ActionResult> Salvare()
        {
            // Se recuperează datele comenzii din TempData
            var detaliiComanda = JsonConvert.DeserializeObject<DetaliiComandaViewModel>((string)TempData["detaliiComanda"]);
            if (detaliiComanda == null || detaliiComanda.comanda == null || detaliiComanda.card == null)
            {
                return Content("Invalid data.");
            }

            // Se recuperează lista produselor din coș (din sesiune)
            var listaProduse = Session["produseCos"] as List<CosCumparaturi>;
            if (listaProduse == null || !listaProduse.Any())
            {
                return Content("Cosul de cumpărături este gol sau a expirat sesiunea.");
            }

            // Verificare card: dacă există și dacă are sold suficient
            var nrCard = detaliiComanda.card.NrCard;
            var numeDetinator = detaliiComanda.card.NumeDetinator;
            var cvc = detaliiComanda.card.CVC;

            var cardDB = await context.carduri
                .SingleOrDefaultAsync(c => c.NrCard == nrCard
                    && c.NumeDetinator == numeDetinator
                    && c.CVC == cvc);

            if (cardDB == null)
            {
                // Cardul nu există — redirectare înapoi cu eroare
                TempData["eroarePlata"] = "Card inexistent";
                TempData["detaliiComanda"] = JsonConvert.SerializeObject(detaliiComanda);
                return RedirectToAction("FinalizareComanda", new { etapa = "Plata", eroare = "da" });
            }
            else if (cardDB.Sold < detaliiComanda.comanda.PretComanda)
            {
                // Sold insuficient — redirectare înapoi cu eroare
                TempData["eroarePlata"] = "Sold insuficient";
                TempData["detaliiComanda"] = JsonConvert.SerializeObject(detaliiComanda);
                return RedirectToAction("FinalizareComanda", new { etapa = "Plata", eroare = "da" });
            }

            // Se retrage suma de pe card
            cardDB.Sold -= detaliiComanda.comanda.PretComanda;

            // Se adaugă factura și adresa în baza de date
            var factura = detaliiComanda.comanda.Factura;
            var adresa = detaliiComanda.comanda.Adresa;
            context.factura.Add(factura);
            context.adrese.Add(adresa);
            await context.SaveChangesAsync(); // pentru a obține ID-urile generate

            // Se completează comanda cu referințele către factura și adresa create
            detaliiComanda.comanda.FacturaId = factura.Id;
            detaliiComanda.comanda.AdresaId = adresa.Id;
            detaliiComanda.comanda.ClientId = User.Identity.GetUserId();

            context.comanda.Add(detaliiComanda.comanda);
            await context.SaveChangesAsync(); // salvăm comanda

            // Se adaugă produsele comandate și se șterg din coș
            foreach (var item in listaProduse)
            {
                var produsComandat = new ProdusComandat
                {
                    ProdusId = item.ProdusId,
                    CategorieId = item.Produs.CategorieId,
                    Cantitate = item.NrBuc,
                    ComandaId = detaliiComanda.comanda.Id
                };
                context.produseComandate.Add(produsComandat);

                // Se elimină produsul din coș
                var cos = await context.cos.FindAsync(item.Id);
                if (cos != null)
                    context.cos.Remove(cos);
            }

            await context.SaveChangesAsync(); // salvăm modificările finale

            return RedirectToAction("FinalizareComanda", "Comanda", new { etapa = "Finalizare" });
        }

        // Afișează pagina finală (sau revine la plata cu eroare)
        public ActionResult FinalizareComanda(string etapa, string eroare)
        {
            if (eroare != null)
            {
                // Se reîncarcă datele comenzii și se revine în view cu eroare
                var detaliiComanda = JsonConvert.DeserializeObject<DetaliiComandaViewModel>((string)TempData["detaliiComanda"]);
                detaliiComanda.etapa = etapa;
                return View("DetaliiComanda", detaliiComanda);
            }

            var viewModel = new DetaliiComandaViewModel()
            {
                etapa = etapa
            };

            TempData["eroarePlata"] = null;
            return View("DetaliiComanda", viewModel);
        }
    }
}
