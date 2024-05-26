using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class SurseDeAlimentareController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: SurseDeAlimentare
        public ActionResult Index()
        {
            if (User.IsInRole("Furnizor"))
            {
                var userId = User.Identity.GetUserId();
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId);
                var surse = context.surse.Where(c => c.Brand == brand.numeBrand).ToList();
                return View(surse);
            }
            else
            {
                var surse = context.surse.ToList();
                var userId = User.Identity.GetUserId();
                foreach (var sursa in surse)
                {
                    var produsFavorit = context.produseFavorite.SingleOrDefault(p => p.idProdus == sursa.Id && p.categorie == "surseDeAlimentare" && p.Utilizator.Id == userId);
                    if (produsFavorit != null)
                    {
                        sursa.produsFavorit = true;
                    }
                    else
                    {
                        sursa.produsFavorit = false;
                    }
                }
                return View(surse);
            }
        }
        public ActionResult Adauga()
        {
            return View();
        }
        public ActionResult Vizualizare(int id)
        {
            var surse = context.surse.SingleOrDefault(s => s.Id == id);
            return View(surse);
        }
        public ActionResult Salvare(PSU model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            model.Brand = brand.numeBrand;
            model.imgLink = "Imagini/SursePC/" + model.imgLink;
            context.surse.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "SurseDeAlimentare");
        }
        public ActionResult Sterge(int id)
        {
            var sursa = context.surse.Single(c => c.Id == id);
            var cos = context.cos.Where(c => c.idProdus == id && c.categorie == "surseDeAlimentare").ToList();
            var favorite = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == "surseDeAlimentare").ToList();
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach (var p in favorite)
            {
                context.produseFavorite.Remove(p);
            }
            context.surse.Remove(sursa);
            context.SaveChanges();
            return RedirectToAction("Index", "SurseDeAlimentare");
        }
        public ActionResult ActualizareStoc(int id, int stocNou)
        {
            context.surse.Single(c => c.Id == id).stoc = stocNou;
            context.SaveChanges();
            return RedirectToAction("Vizualizare", "SurseDeAlimentare", new { id });
        }
    }
}