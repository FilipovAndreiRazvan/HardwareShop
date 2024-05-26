using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class CarcaseController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Carcase
        public ActionResult Index()
        {

            if (User.IsInRole("Furnizor"))
            {
                var userId = User.Identity.GetUserId();
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId);
                var carcase = context.carcase.Where(c => c.Brand == brand.numeBrand).ToList();
                return View(carcase);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                List<Carcasa> carcase = context.carcase.ToList();

                foreach (var carcasa in carcase)
                {
                    var produsFavorit = context.produseFavorite.SingleOrDefault(p => p.idProdus == carcasa.Id && p.categorie == "carcase" && p.Utilizator.Id == userId);
                    if (produsFavorit != null)
                    {
                        carcasa.produsFavorit = true;
                    }
                    else
                    {
                        carcasa.produsFavorit = false;
                    }
                }
                return View(carcase);
            }


        }
        public ActionResult Vizualizare(int id)
        {
            var carcasa = context.carcase.SingleOrDefault(c => c.Id == id);
            return View(carcasa);
        }

        public ActionResult Adauga()
        {
            return View();
        }
        public ActionResult Salvare(Carcasa model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            model.Brand = brand.numeBrand;
            model.imgLink = "Imagini/Carcase/" + model.imgLink;
            context.carcase.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "Carcase");
        }
        public ActionResult ActualizareStoc(int id, int stocNou)
        {
            context.carcase.Single(c => c.Id == id).stoc = stocNou;
            context.SaveChanges();
            return RedirectToAction("Vizualizare", "Carcase", new { id });
        }

        public ActionResult Sterge(int id)
        {
            var carcasa = context.carcase.Single(c => c.Id == id);
            var cos = context.cos.Where(c => c.idProdus == id && c.categorie == "carcase").ToList();
            var favorite = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == "carcase").ToList();
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach (var p in favorite)
            {
                context.produseFavorite.Remove(p);
            }
            context.carcase.Remove(carcasa);
            context.SaveChanges();
            return RedirectToAction("Index", "Carcase");
        }
    }
}