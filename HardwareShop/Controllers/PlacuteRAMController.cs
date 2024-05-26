using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class PlacuteRAMController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: PlacuteRAM
        public ActionResult Index()
        {
            if (User.IsInRole("Furnizor"))
            {
                var userId = User.Identity.GetUserId();
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId);
                var placuteRAM = context.placuteRAM.Where(c => c.Brand == brand.numeBrand).ToList();
                return View(placuteRAM);
            }
            else
            {
                var placuteRAM = context.placuteRAM.ToList();
                var userId = User.Identity.GetUserId();
                foreach (var placaRAM in placuteRAM)
                {
                    var produsFavorit = context.produseFavorite.SingleOrDefault(p => p.idProdus == placaRAM.Id && p.categorie == "placuteRAM" && p.Utilizator.Id == userId);
                    if (produsFavorit != null)
                    {
                        placaRAM.produsFavorit = true;
                    }
                    else
                    {
                        placaRAM.produsFavorit = false;
                    }
                }
                return View(placuteRAM);
            }
        }

        public ActionResult Vizualizare(int id)
        {
            var placutaRAM = context.placuteRAM.SingleOrDefault(p => p.Id == id);
            return View(placutaRAM);
        }
        public ActionResult Adauga()
        {
            return View();
        }
        public ActionResult Salvare(PlacutaRAM model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            model.Brand = brand.numeBrand;
            model.imgLink = "Imagini/PlacuteRAM/" + model.imgLink;
            context.placuteRAM.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "PlacuteRAM");
        }
        public ActionResult ActualizareStoc(int id, int stocNou)
        {
            context.placuteRAM.Single(c => c.Id == id).stoc = stocNou;
            context.SaveChanges();
            return RedirectToAction("Vizualizare", "PlacuteRAM", new { id });
        }

        public ActionResult Sterge(int id)
        {
            var placuta = context.placuteRAM.Single(c => c.Id == id);
            var cos = context.cos.Where(c => c.idProdus == id && c.categorie == "placuteRAM").ToList();
            var favorite = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == "placuteRAM").ToList();
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach (var p in favorite)
            {
                context.produseFavorite.Remove(p);
            }
            context.placuteRAM.Remove(placuta);
            context.SaveChanges();
            return RedirectToAction("Index", "PlacuteRAM");
        }
    }
}