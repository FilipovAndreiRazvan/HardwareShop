using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    [Authorize(Roles = "Client")]
    public class ComandaController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Comanda

        public ActionResult DetaliiComanda(float? pret)
        {
            Comanda comanda = new Comanda();
            comanda.pretComanda = (float)pret;
            var viewModel = new DetaliiComandaViewModel()
            {
                comanda = comanda,
                etapa = "Facturare"
            };
            return View(viewModel);
        }
        public ActionResult FinalizareComanda(string etapa, string detaliiComandaSerializat, string eroare)
        {
            if (eroare != null)
            {
                var detaliiComanda = JsonConvert.DeserializeObject<DetaliiComandaViewModel>(detaliiComandaSerializat);
                detaliiComanda.etapa = etapa;
                return View("DetaliiComanda", detaliiComanda);
            }
            var viewModel = new DetaliiComandaViewModel()
            {
                etapa = etapa
            };
            Session["eroarePlata"] = null;
            return View("DetaliiComanda", viewModel);
        }
        [HttpPost]
        public ActionResult DetaliiComanda(string etapa, Comanda comanda, DetaliiComandaViewModel detaliiComanda, string eroare)
        {
            if (detaliiComanda.comanda == null || eroare != null)
            {
                detaliiComanda.comanda = comanda;
                detaliiComanda.etapa = etapa;
                return View(detaliiComanda);
            }
            var serializedData = JsonConvert.SerializeObject(detaliiComanda);
            return RedirectToAction("Salvare", "Comanda", new { serializedData });
        }
        public ActionResult Salvare(string serializedData)
        {
            var detaliiComanda = JsonConvert.DeserializeObject<DetaliiComandaViewModel>(serializedData);
            if (detaliiComanda == null || detaliiComanda.comanda == null || detaliiComanda.card == null)
            {
                return Content("Invalid data.");
            }

            var listaProduse = Session["produseCos"] as List<CosCumparaturi>;
            foreach (var item in listaProduse)
            {
                var cos = context.cos.Find(item.Id);
                context.cos.Remove(cos);
                var produsComandat = new ProduseComandate
                {
                    IdProdus = item.idProdus,
                    Categorie = item.categorie,
                    Cantitate = item.nrBuc
                };
                produsComandat.IdComanda = detaliiComanda.comanda;
                context.produseComandate.Add(produsComandat);
            }

            var nrCard = detaliiComanda.card.nrCard;
            var numeDetinator = detaliiComanda.card.numeDetinator;
            var cvc = detaliiComanda.card.CVC;

            var cardDB = context.carduri
                                  .SingleOrDefault(c => c.nrCard == nrCard
                                                     && c.numeDetinator == numeDetinator
                                                     && c.CVC == cvc);

            detaliiComanda.comanda.pretComanda = JsonConvert.DeserializeObject<float>(detaliiComanda.comanda.pretComandaJson);

            if (cardDB == null)
            {
                var detaliiComandaSerializat = JsonConvert.SerializeObject(detaliiComanda);
                Session["eroarePlata"] = "Card inexistent";
                return RedirectToAction("FinalizareComanda", new { detaliiComandaSerializat, etapa = "Plata", eroare = "da" });
            }
            else if (cardDB.sold < detaliiComanda.comanda.pretComanda)
            {
                var detaliiComandaSerializat = JsonConvert.SerializeObject(detaliiComanda);
                Session["eroarePlata"] = "Sold insuficient";
                return RedirectToAction("FinalizareComanda", new { detaliiComandaSerializat, etapa = "Plata", eroare = "da" });
            }
            else
            {
                cardDB.sold -= detaliiComanda.comanda.pretComanda;
                var factura = JsonConvert.DeserializeObject<Factura>(detaliiComanda.comanda.FacturaJson);
                var adresa = JsonConvert.DeserializeObject<AdresaLivrare>(detaliiComanda.comanda.AdresaJson);

                detaliiComanda.comanda.adresa = adresa;
                detaliiComanda.comanda.factura = factura;
                string userId = User.Identity.GetUserId();
                var user = context.Users.Find(userId);
                detaliiComanda.comanda.client = user;

                context.factura.Add(factura);
                context.comanda.Add(detaliiComanda.comanda);
                context.SaveChanges();

                return RedirectToAction("FinalizareComanda", "Comanda", new { etapa = "Finalizare" });
            }
        }


    }
}