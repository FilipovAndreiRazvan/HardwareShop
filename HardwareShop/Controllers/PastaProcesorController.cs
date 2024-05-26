using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class PastaProcesorController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: PastaProcesor
        public ActionResult Index()
        {
            if (User.IsInRole("Furnizor"))
            {
                var userId = User.Identity.GetUserId();
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId);
                var pasta = context.pasteProcesor.Where(c => c.Brand == brand.numeBrand).ToList();
                return View(pasta);
            }
            else
            {
                var pasta = context.pasteProcesor.ToList();
                var userId = User.Identity.GetUserId();
                foreach (var item in pasta)
                {
                    var produsFavorit = context.produseFavorite.SingleOrDefault(p => p.idProdus == item.Id && p.categorie == "pastaCPU" && p.Utilizator.Id == userId);
                    if (produsFavorit != null)
                    {
                        item.produsFavorit = true;
                    }
                    else
                    {
                        item.produsFavorit = false;
                    }
                }
                return View(pasta);
            }
        }

        public ActionResult Vizualizare(int id)
        {
            var pasta = context.pasteProcesor.SingleOrDefault(p => p.Id == id);
            return View(pasta);
        }
        public ActionResult Adauga()
        {
            return View();
        }
        public ActionResult Salvare(PastaCPU model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            model.Brand = brand.numeBrand;
            model.imgLink = "Imagini/PasteProcesor/" + model.imgLink;
            context.pasteProcesor.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "PastaProcesor");
        }
        public ActionResult ActualizareStoc(int id, int stocNou)
        {
            context.pasteProcesor.Single(c => c.Id == id).stoc = stocNou;
            context.SaveChanges();
            return RedirectToAction("Vizualizare", "PastaProcesor", new { id });
        }
        public ActionResult Sterge(int id)
        {
            var pasta = context.pasteProcesor.Single(c => c.Id == id);
            var cos = context.cos.Where(c => c.idProdus == id && c.categorie == "pastaCPU").ToList();
            var favorite = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == "pastaCPU").ToList();
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach (var p in favorite)
            {
                context.produseFavorite.Remove(p);
            }
            context.pasteProcesor.Remove(pasta);
            context.SaveChanges();
            return RedirectToAction("Index", "PastaProcesor");
        }
    }
}