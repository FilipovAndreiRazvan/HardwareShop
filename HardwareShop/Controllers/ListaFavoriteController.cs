using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    [Authorize(Roles = "Client")]
    public class ListaFavoriteController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: ListaFavorite
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var produseFavorite = context.produseFavorite.Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).Where(p =>p.UtilizatorId == userId).ToList();
            return View(produseFavorite);
        }


        public ActionResult Adauga(int ProdusId,bool paginaOferte = false)
        {
            var userId = User.Identity.GetUserId();
            var produs = context.produse.Include(p => p.Categorie).SingleOrDefault(p => p.IdProdus == ProdusId);
            var produsFavorit = new ProdusFavorit();
            produsFavorit.ProdusId = ProdusId;
            produsFavorit.UtilizatorId = userId;
            context.produseFavorite.Add(produsFavorit);
            context.SaveChanges();
            TempData["Mesaj"] = "ProdusAdaugatListaFavorite";

            return RedirectToAction("Index", "Servicii", new {categorie = produs.Categorie.Nume,paginaOferte = paginaOferte});
        }
        public ActionResult Sterge(int ProdusId,string viewName,bool paginaOferte = false)
        {
            var userId = User.Identity.GetUserId();
            var produsFavorit = context.produseFavorite.Include(p => p.Produs.Categorie).FirstOrDefault(p => p.ProdusId == ProdusId && p.UtilizatorId == userId);
            var categorie = produsFavorit.Produs.Categorie.Nume;
            context.produseFavorite.Remove(produsFavorit);
            context.SaveChanges();
            if (viewName == "listaFavorite")
            {
                return RedirectToAction("Index", "ListaFavorite");
            }

            TempData["Mesaj"] = "ProdusStersListaFavorite";
            return RedirectToAction("Index", "Servicii", new { categorie = categorie,paginaOferte = paginaOferte});
        }
    }
}