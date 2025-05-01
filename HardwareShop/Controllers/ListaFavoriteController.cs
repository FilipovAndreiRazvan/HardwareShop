using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    // Restricționează accesul doar pentru utilizatori cu rolul "Client"
    [Authorize(Roles = "Client")]
    public class ListaFavoriteController : Controller
    {
        private readonly ApplicationDbContext context;

        // Constructor care primește contextul bazei de date prin dependency injection
        public ListaFavoriteController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Afișează lista de produse favorite ale utilizatorului logat
        public async Task<ActionResult> Index()
        {
            // Obține ID-ul utilizatorului curent
            var userId = User.Identity.GetUserId();

            // Selectează toate produsele favorite care aparțin utilizatorului, incluzând și informații despre categorie și brand
            var produseFavorite = await context.produseFavorite
                .Include(p => p.Produs.Categorie)
                .Include(p => p.Produs.Brand)
                .Where(p => p.UtilizatorId == userId)
                .ToListAsync();

            // Trimite lista către view
            return View(produseFavorite);
        }

        // Adaugă un produs la lista de favorite
        public async Task<ActionResult> Adauga(int ProdusId, bool paginaOferte = false)
        {
            var userId = User.Identity.GetUserId();

            // Caută produsul după ID și încarcă și categoria acestuia
            var produs = await context.produse
                .Include(p => p.Categorie)
                .SingleOrDefaultAsync(p => p.IdProdus == ProdusId);

            // Creează o nouă entitate de tip produs favorit
            var produsFavorit = new ProdusFavorit
            {
                ProdusId = ProdusId,
                UtilizatorId = userId
            };

            // Verifică dacă produsul există
            if (produs == null)
            {
                throw new HttpException(404, "Produs inexistent!");
            }

            // Adaugă produsul favorit și salvează în baza de date
            context.produseFavorite.Add(produsFavorit);
            await context.SaveChangesAsync();

            // Trimite mesaj de confirmare în TempData
            TempData["Mesaj"] = "ProdusAdaugatListaFavorite";

            // Redirecționează înapoi către lista de produse, păstrând categoria și sursa (pagina de oferte sau nu)
            return RedirectToAction("Index", "Servicii", new { categorie = produs.Categorie.Nume, paginaOferte = paginaOferte });
        }

        // Șterge un produs din lista de favorite
        public async Task<ActionResult> Sterge(int ProdusId, string viewName, bool paginaOferte = false)
        {
            var userId = User.Identity.GetUserId();

            // Caută produsul favorit care aparține utilizatorului curent
            var produsFavorit = await context.produseFavorite
                .Include(p => p.Produs.Categorie)
                .FirstOrDefaultAsync(p => p.ProdusId == ProdusId && p.UtilizatorId == userId);

            // Dacă produsul nu e găsit în lista de favorite, se aruncă eroare
            if (produsFavorit == null)
            {
                throw new HttpException(404, "Produsul nu se afla in lista de favorite!");
            }

            // Obține categoria produsului pentru redirect
            var categorie = produsFavorit.Produs.Categorie.Nume;

            // Șterge produsul din lista de favorite și salvează modificările
            context.produseFavorite.Remove(produsFavorit);
            await context.SaveChangesAsync();

            // Dacă s-a cerut redirect spre lista de favorite, mergem acolo
            if (viewName == "listaFavorite")
            {
                return RedirectToAction("Index", "ListaFavorite");
            }

            // Alt redirect spre pagina produselor
            TempData["Mesaj"] = "ProdusStersListaFavorite";
            return RedirectToAction("Index", "Servicii", new { categorie = categorie, paginaOferte = paginaOferte });
        }
    }
}
