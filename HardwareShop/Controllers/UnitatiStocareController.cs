using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class UnitatiStocareController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: UnitatiDeStocare
        public ActionResult Index()
        {
            if (User.IsInRole("Furnizor"))
            {
                var userId = User.Identity.GetUserId();
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId);
                var unitatiStocare = context.stocare.Where(c => c.Brand == brand.numeBrand).ToList();
                return View(unitatiStocare);
            }
            else
            {
                var unitatiStocare = context.stocare.ToList();
                var userId = User.Identity.GetUserId();
                foreach (var unitateStocare in unitatiStocare)
                {
                    var produsFavorit = context.produseFavorite.SingleOrDefault(p => p.idProdus == unitateStocare.Id && p.categorie == "unitatiDeStocare" && p.Utilizator.Id == userId);
                    if (produsFavorit != null)
                    {
                        unitateStocare.produsFavorit = true;
                    }
                    else
                    {
                        unitateStocare.produsFavorit = false;
                    }
                }
                return View(unitatiStocare);
            }
        }

        public ActionResult Vizualizare(int id)
        {
            var unitateStocare = context.stocare.SingleOrDefault(uS => uS.Id == id);
            return View(unitateStocare);
        }
        public ActionResult Adauga()
        {
            return View();
        }
        public ActionResult Salvare(UnitatiDeStocare model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            model.Brand = brand.numeBrand;
            model.imgLink = "Imagini/UnitatiDeStocare/" + model.imgLink;
            context.stocare.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "UnitatiStocare");
        }
        public ActionResult ActualizareStoc(int id, int stocNou)
        {
            context.stocare.Single(c => c.Id == id).stoc = stocNou;
            context.SaveChanges();
            return RedirectToAction("Vizualizare", "UnitatiStocare", new { id });
        }
        public ActionResult Sterge(int id)
        {
            var unitate = context.stocare.Single(c => c.Id == id);
            var cos = context.cos.Where(c => c.idProdus == id && c.categorie == "unitatiDeStocare").ToList();
            var favorite = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == "unitatiDeStocare").ToList();
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach (var p in favorite)
            {
                context.produseFavorite.Remove(p);
            }
            context.stocare.Remove(unitate);
            context.SaveChanges();
            return RedirectToAction("Index", "UnitatiStocare");
        }
    }
}