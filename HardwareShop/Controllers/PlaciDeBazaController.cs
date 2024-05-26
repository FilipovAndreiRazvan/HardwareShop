using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class PlaciDeBazaController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: PlacaDeBaza
        public ActionResult Index()
        {
            if (User.IsInRole("Furnizor"))
            {
                var userId = User.Identity.GetUserId();
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId);
                var placiDeBaza = context.placiDeBaza.Where(c => c.Brand == brand.numeBrand).ToList();
                return View(placiDeBaza);
            }
            else
            {
                var placiDeBaza = context.placiDeBaza.ToList();
                var userId = User.Identity.GetUserId();
                foreach (var placaDeBaza in placiDeBaza)
                {
                    var produsFavorit = context.produseFavorite.SingleOrDefault(p => p.idProdus == placaDeBaza.Id && p.categorie == "placiDeBaza" && p.Utilizator.Id == userId);
                    if (produsFavorit != null)
                    {
                        placaDeBaza.produsFavorit = true;
                    }
                    else
                    {
                        placaDeBaza.produsFavorit = false;
                    }
                }
                return View(placiDeBaza);
            }
        }

        public ActionResult Vizualizare(int id)
        {
            var placa = context.placiDeBaza.SingleOrDefault(p => p.Id == id);
            return View(placa);
        }
        public ActionResult Adauga()
        {
            return View();
        }
        public ActionResult Salvare(Motherboard model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            model.Brand = brand.numeBrand;
            model.imgLink = "Imagini/PlaciDeBaza/" + model.imgLink;
            context.placiDeBaza.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "PlaciDeBaza");
        }

        public ActionResult Sterge(int id)
        {
            var placaDeBaza = context.placiDeBaza.Single(c => c.Id == id);
            var cos = context.cos.Where(c => c.idProdus == id && c.categorie == "placiDeBaza").ToList();
            var favorite = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == "placiDeBaza").ToList();
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach (var p in favorite)
            {
                context.produseFavorite.Remove(p);
            }
            context.placiDeBaza.Remove(placaDeBaza);
            context.SaveChanges();
            return RedirectToAction("Index", "PlaciDeBaza");
        }
        public ActionResult ActualizareStoc(int id, int stocNou)
        {
            context.placiDeBaza.Single(c => c.Id == id).stoc = stocNou;
            context.SaveChanges();
            return RedirectToAction("Vizualizare", "PlaciDeBaza", new { id });
        }
    }
}