using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class ProcesoareController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Procesoare
        public ActionResult Index()
        {
            if (User.IsInRole("Furnizor"))
            {
                var userId = User.Identity.GetUserId();
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId);
                var procesoare = context.procesoare.Where(c => c.Brand == brand.numeBrand).ToList();
                return View(procesoare);
            }
            else
            {
                var procesoare = context.procesoare.ToList();
                var userId = User.Identity.GetUserId();
                foreach (var procesor in procesoare)
                {
                    var produsFavorit = context.produseFavorite.SingleOrDefault(p => p.idProdus == procesor.Id && p.categorie == "procesoare" && p.Utilizator.Id == userId);
                    if (produsFavorit != null)
                    {
                        procesor.produsFavorit = true;
                    }
                    else
                    {
                        procesor.produsFavorit = false;
                    }
                }
                return View(procesoare);
            }
        }

        public ActionResult Vizualizare(int id)
        {
            var procesor = context.procesoare.SingleOrDefault(p => p.Id == id);
            return View(procesor);
        }

        public ActionResult Adauga()
        {
            return View();
        }

        public ActionResult Salvare(CPU model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            model.Brand = brand.numeBrand;
            model.imgLink = "Imagini/Procesoare/" + model.imgLink;
            context.procesoare.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "Procesoare");
        }
        public ActionResult ActualizareStoc(int id, int stocNou)
        {
            context.procesoare.Single(c => c.Id == id).stoc = stocNou;
            context.SaveChanges();
            return RedirectToAction("Vizualizare", "Procesoare", new { id });
        }

        public ActionResult Sterge(int id)
        {
            var procesor = context.procesoare.Single(c => c.Id == id);
            var cos = context.cos.Where(c => c.idProdus == id && c.categorie == "procesoare").ToList();
            var favorite = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == "procesoare").ToList();
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach (var p in favorite)
            {
                context.produseFavorite.Remove(p);
            }
            context.procesoare.Remove(procesor);
            context.SaveChanges();
            return RedirectToAction("Index", "Procesoare");
        }
    }
}