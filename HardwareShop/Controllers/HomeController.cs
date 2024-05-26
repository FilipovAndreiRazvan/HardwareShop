using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

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
        public ActionResult Index(bool adaugaProdusCos = false, bool adaugaProdusFavorit = false)
        {
            var userId = User.Identity.GetUserId();
            var produsAdaugat = new AdaugaProdus()
            {
                cosCumparaturi = adaugaProdusCos,
                listaFavorite = adaugaProdusFavorit,
                cerere = userId == null ? null : context.cereri.SingleOrDefault(c => c.Utilizator.Id == userId && c.Status == "Acceptata")

            };
            return View(produsAdaugat);
        }

        [Authorize(Roles = "Client")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}