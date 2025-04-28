using HardwareShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace HardwareShop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context;

        public HomeController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult ContClient(string nume)
        {
            ApplicationUser user = context.Users.SingleOrDefault(u => u.UserName == nume);

            return View(user);
        }
        public ActionResult Index(bool paginaOferte = false)
        {
            if(paginaOferte == true)
            {
                TempData["Mesaj"] = null;
            }
            var userId = User.Identity.GetUserId();
            List<Produs> oferte = context.produse.Include(p => p.Categorie).Where(p => p.Oferta != null).ToList();
            //List<Produs> oferte = context.produse.Include(p => p.Categorie).ToList();
            foreach (Produs produs in oferte)
            {
                produs.ProdusFavorit = context.produseFavorite.Any(p => p.ProdusId == produs.IdProdus && p.UtilizatorId == userId); 
            }
            return View(oferte);
        }
    }
}