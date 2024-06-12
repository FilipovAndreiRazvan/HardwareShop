using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    [Authorize(Roles = "Client")]
    public class ListaFavoriteController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: ListaFavorite
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var produseFavoriteDb = context.produseFavorite.Include(p => p.Utilizator).Where(p => p.Utilizator.Id == userId).ToList();
            var produseFavorite = new ProduseFavoriteViewModel();
            if (produseFavoriteDb.Count == 0)
            {
                produseFavorite.Id = 0;
                return View(produseFavorite);
            }
            else
            {
                produseFavorite.Id = 1;
            }

            foreach (var item in produseFavoriteDb)
            {
                if (item.categorie == "carcase")
                {
                    var carcasa = context.carcase.SingleOrDefault(c => c.Id == item.idProdus);
                    produseFavorite.carcase.Add(carcasa);
                }
                else if (item.categorie == "placiDeBaza")
                {
                    var placaDeBaza = context.placiDeBaza.SingleOrDefault(p => p.Id == item.idProdus);
                    produseFavorite.motherboard.Add(placaDeBaza);
                }
                else if (item.categorie == "procesoare")
                {
                    var procesor = context.procesoare.SingleOrDefault(p => p.Id == item.idProdus);
                    produseFavorite.cpu.Add(procesor);
                }
                else if (item.categorie == "placiVideo")
                {
                    var placaVideo = context.placiVideo.SingleOrDefault(p => p.Id == item.idProdus);
                    produseFavorite.gpu.Add(placaVideo);
                }
                else if (item.categorie == "placuteRAM")
                {
                    var placutaRAM = context.placuteRAM.SingleOrDefault(p => p.Id == item.idProdus);
                    produseFavorite.placuteRAM.Add(placutaRAM);
                }
                else if (item.categorie == "unitatiDeStocare")
                {
                    var unitateStocare = context.stocare.SingleOrDefault(p => p.Id == item.idProdus);
                    produseFavorite.unitatiDeStocare.Add(unitateStocare);
                }
                else if (item.categorie == "surseDeAlimentare")
                {
                    var sursa = context.surse.SingleOrDefault(p => p.Id == item.idProdus);
                    produseFavorite.psu.Add(sursa);
                }
                else if (item.categorie == "pastaCPU")
                {
                    var pastaCPU = context.pasteProcesor.SingleOrDefault(p => p.Id == item.idProdus);
                    produseFavorite.pastaCPU.Add(pastaCPU);
                }
            }
            return View(produseFavorite);
        }


        public ActionResult Adauga(int id, string categorie)
        {
            var userId = User.Identity.GetUserId();
            var produsFavoritDb = context.produseFavorite.Where(p => p.idProdus == id && p.categorie == categorie && p.Utilizator.Id == userId).ToList();
            ApplicationUser utilizatorAutentificat = context.Users.Find(userId);
            if (produsFavoritDb.Count == 0)
            {
                var produsFavorit = new ProdusFavorit();
                produsFavorit.categorie = categorie;
                produsFavorit.idProdus = id;
                produsFavorit.Utilizator = utilizatorAutentificat;

                context.produseFavorite.Add(produsFavorit);
                if (categorie == "carcase")
                {
                    context.carcase.SingleOrDefault(p => p.Id == id).produsFavorit = true;
                }
                else if (categorie == "placiDeBaza")
                {
                    context.placiDeBaza.SingleOrDefault(p => p.Id == id).produsFavorit = true;
                }
                else if (categorie == "procesoare")
                {
                    context.procesoare.SingleOrDefault(p => p.Id == id).produsFavorit = true;
                }
                else if (categorie == "placiVideo")
                {
                    context.placiVideo.SingleOrDefault(p => p.Id == id).produsFavorit = true;
                }
                else if (categorie == "placuteRAM")
                {
                    context.placuteRAM.SingleOrDefault(p => p.Id == id).produsFavorit = true;
                }
                else if (categorie == "unitatiDeStocare")
                {
                    context.stocare.SingleOrDefault(p => p.Id == id).produsFavorit = true;
                }
                else if (categorie == "surseDeAlimentare")
                {
                    context.surse.SingleOrDefault(p => p.Id == id).produsFavorit = true;
                }
                else if (categorie == "pastaCPU")
                {
                    context.pasteProcesor.SingleOrDefault(p => p.Id == id).produsFavorit = true;
                }
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Home", new { adaugaProdusFavorit = true });
        }
        public ActionResult Sterge(int id, string categorie, string viewName)
        {
            var userId = User.Identity.GetUserId();
            context.produseFavorite.Remove(context.produseFavorite.SingleOrDefault(p => p.idProdus == id && p.categorie == categorie && p.Utilizator.Id == userId));
            if (categorie == "carcase")
            {
                context.carcase.SingleOrDefault(p => p.Id == id).produsFavorit = false;
            }
            else if (categorie == "placiDeBaza")
            {
                context.placiDeBaza.SingleOrDefault(p => p.Id == id).produsFavorit = false;
            }
            else if (categorie == "procesoare")
            {
                context.procesoare.SingleOrDefault(p => p.Id == id).produsFavorit = false;
            }
            else if (categorie == "placiVideo")
            {
                context.placiVideo.SingleOrDefault(p => p.Id == id).produsFavorit = false;
            }
            else if (categorie == "placuteRAM")
            {
                context.placuteRAM.SingleOrDefault(p => p.Id == id).produsFavorit = false;
            }
            else if (categorie == "unitatiDeStocare")
            {
                context.stocare.SingleOrDefault(p => p.Id == id).produsFavorit = false;
            }
            else if (categorie == "surseDeAlimentare")
            {
                context.surse.SingleOrDefault(p => p.Id == id).produsFavorit = false;
            }
            else if (categorie == "pastaCPU")
            {
                context.pasteProcesor.SingleOrDefault(p => p.Id == id).produsFavorit = false;
            }
            context.SaveChanges();
            if (viewName == "listaFavorite")
            {
                return RedirectToAction("Index", "ListaFavorite");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}