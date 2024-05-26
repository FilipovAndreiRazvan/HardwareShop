using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class PlaciVideoController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: PlaciVideo
        public ActionResult Index()
        {
            if (User.IsInRole("Furnizor"))
            {
                var userId = User.Identity.GetUserId();
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId);
                var placiVideo = context.placiVideo.Where(c => c.Brand == brand.numeBrand).ToList();
                return View(placiVideo);
            }
            else
            {
                var placiVideo = context.placiVideo.ToList();
                var userId = User.Identity.GetUserId();
                foreach (var placaVideo in placiVideo)
                {
                    var produsFavorit = context.produseFavorite.SingleOrDefault(p => p.idProdus == placaVideo.Id && p.categorie == "placiVideo" && p.Utilizator.Id == userId);
                    if (produsFavorit != null)
                    {
                        placaVideo.produsFavorit = true;
                    }
                    else
                    {
                        placaVideo.produsFavorit = false;
                    }
                }
                return View(placiVideo);
            }
        }

        public ActionResult Vizualizare(int id)
        {
            var placaVideo = context.placiVideo.SingleOrDefault(pV => pV.Id == id);
            return View(placaVideo);
        }
        public ActionResult Adauga()
        {
            return View();
        }
        public ActionResult Salvare(GPU model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            model.Brand = brand.numeBrand;
            model.imgLink = "Imagini/PlaciVideo/" + model.imgLink;
            context.placiVideo.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "PlaciVideo");
        }

        public ActionResult Sterge(int id)
        {
            var placaVideo = context.placiVideo.Single(c => c.Id == id);
            var cos = context.cos.Where(c => c.idProdus == id && c.categorie == "placiVideo").ToList();
            var favorite = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == "placiVideo").ToList();
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach (var p in favorite)
            {
                context.produseFavorite.Remove(p);
            }
            context.placiVideo.Remove(placaVideo);
            context.SaveChanges();
            return RedirectToAction("Index", "PlaciVideo");
        }
        public ActionResult ActualizareStoc(int id, int stocNou)
        {
            context.placiVideo.Single(c => c.Id == id).stoc = stocNou;
            context.SaveChanges();
            return RedirectToAction("Vizualizare", "PlaciVideo", new { id });
        }
    }
}