using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using HardwareShop.ViewModels;
using Microsoft.Ajax.Utilities;
using System.Data.Entity.Validation;
using System.Drawing.Drawing2D;

namespace HardwareShop.Controllers
{
    public class ServiciiController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        
        public ActionResult Index(string categorie,bool index = false,bool paginaOferte = false)
        {
            if(paginaOferte)
            {
                return RedirectToAction("Index", "Home");
            }
            if (index)
            {
                TempData["Mesaj"] = null;
            }
            var userId = User.Identity.GetUserId();
            var metodeExterne = new MetodeExterne(context);
            var model = (IndexProdusViewModel)metodeExterne.ListaProduse(userId, categorie);
            if (User.IsInRole("Furnizor"))
            {
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId)?.NumeBrand;

                if (brand != null)
                {
                    model = (IndexProdusViewModel)metodeExterne.ListaProduse(userId, categorie, numeBrand: brand);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            model.categorieProduse = categorie;
            if (model == null)
                {
                    return HttpNotFound();
                }
        
            return View(model);
            }

        public ActionResult Vizualizare(int id,string categorie)
        {
            var userId = User.Identity.GetUserId();
            var metodeExterne = new MetodeExterne(context);
            var produs = metodeExterne.ListaProduse(userId,categorie, produsId: id);
            var model = metodeExterne.CompletareModel(produs, new AdaugaProdusViewModel());
            if(model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Salvare(AdaugaProdusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            var categorie = context.categorie.Single(c => c.Id == model.Produs.CategorieId);
            var produs = context.produse.SingleOrDefault(p => p.IdProdus == model.Produs.IdProdus);
            
            if(brand == null || categorie == null || produs == null)
            {
                return HttpNotFound();
            }
            model.Produs.Categorie = categorie;
            var metodeExterne = new MetodeExterne(context);
            if(model.Produs.IdProdus != 0)
            {
                TempData["Mesaj"] = "ProdusModificat";
            }
            else
            {
                TempData["Mesaj"] = "ProdusAdaugat";
            }
            metodeExterne.SalvareProdus(model, produs, metodeExterne, brand);
            
            context.SaveChanges();
           
            return RedirectToAction("Index", new {categorie = model.Produs.Categorie.Nume});
        }



        public ActionResult ViewAdauga(string categorie, int? idProdus)
        {
            var model = new AdaugaProdusViewModel
            {
                Produs = new Produs()
            };

            if (idProdus.HasValue)
            {
                var produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                if (produs == null)
                {
                    return HttpNotFound();
                }

                model.Produs = produs;

                switch (categorie)
                {
                    case "Carcase":
                        model.Carcasa = context.carcase.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        break;
                    case "PlaciDeBaza":
                        model.PlacaDeBaza = context.placiDeBaza.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        break;
                    case "PlaciVideo":
                        model.PlacaVideo = context.placiVideo.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        break;
                    case "PlacuteRAM":
                        model.PlacutaRAM = context.placuteRAM.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        break;
                    case "Procesoare":
                        model.Procesor = context.procesoare.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        break;
                    case "SurseDeAlimentare":
                        model.Sursa = context.surse.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        break;
                    case "UnitatiStocare":
                        model.Stocare = context.stocare.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        break;
                    case "PastaProcesor":
                        model.Pasta = context.pasteProcesor.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        break;
                    default:
                        return HttpNotFound();
                }

                return View(model);
            }
            else
            {
                // Cazul de adaugare produs nou
                switch (categorie)
                {
                    case "Carcase":
                        model.Carcasa = new Carcasa();
                        model.Produs.CategorieId = 1;
                        break;
                    case "PlaciDeBaza":
                        model.PlacaDeBaza = new Motherboard();
                        model.Produs.CategorieId = 2;
                        break;
                    case "PlaciVideo":
                        model.PlacaVideo = new GPU();
                        model.Produs.CategorieId = 3;
                        break;
                    case "PlacuteRAM":
                        model.PlacutaRAM = new PlacutaRAM();
                        model.Produs.CategorieId = 4;
                        break;
                    case "Procesoare":
                        model.Procesor = new CPU();
                        model.Produs.CategorieId = 5;
                        break;
                    case "SurseDeAlimentare":
                        model.Sursa = new PSU();
                        model.Produs.CategorieId = 6;
                        break;
                    case "UnitatiStocare":
                        model.Stocare = new UnitatiDeStocare();
                        model.Produs.CategorieId = 7;
                        break;
                    case "PastaProcesor":
                        model.Pasta = new PastaCPU();
                        model.Produs.CategorieId = 8;
                        break;
                    default:
                        return HttpNotFound();
                }

                return View(model);
            }
        }

        public ActionResult Sterge(int id)
        {
            var produs = context.produse.Include(p => p.Categorie).SingleOrDefault(c => c.IdProdus == id);
            if(produs == null)
            {
                return HttpNotFound();
            }
            var categorie = produs.Categorie.Nume;
            var cos = context.cos.Where(c =>c.ProdusId == produs.IdProdus).ToList();
            var listaFavorite = context.produseFavorite.Where(c =>c.ProdusId == produs.IdProdus).ToList();

            context.cos.RemoveRange(cos);
            context.produseFavorite.RemoveRange(listaFavorite);         
            context.produse.Remove(produs);
            context.SaveChanges();

            TempData["Mesaj"] = "ProdusSters";
            return RedirectToAction("Index",new { categorie = categorie });
        }

    }
}