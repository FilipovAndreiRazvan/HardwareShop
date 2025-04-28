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

namespace HardwareShop.Controllers
{
    public class ServiciiController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        
        //Afisam produsele in functie de categoria acestora
        public ActionResult Index(string categorie,bool index = false,bool paginaOferte = false)
        {
            if(paginaOferte == true)
            {
                return RedirectToAction("Index", "Home");
            }
            if (index == true)
            {
                TempData["Mesaj"] = null;
            }
            var userId = User.Identity.GetUserId();
            var metodeExterne = new MetodeExterne(context);
            IndexProdusViewModel model = new IndexProdusViewModel();
            if (User.IsInRole("Furnizor"))
            {
                var brand = context.branduri.SingleOrDefault(b => b.User.Id == userId).NumeBrand;
                
                model = (IndexProdusViewModel)metodeExterne.ListaProduse(userId,categorie, numeBrand:brand);
                model.categorieProduse = categorie;
                if (model == null)
                {
                    return HttpNotFound();
                }
             //lista de produse apartinand unui anumit brand
                    return View(model);
            }
            else
            {
                model = (IndexProdusViewModel)metodeExterne.ListaProduse(userId, categorie);
                model.categorieProduse = categorie;
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
        }


        //vizualizarea unui singur produs
        public ActionResult Vizualizare(int id,string categorie)
        {
            var userId = User.Identity.GetUserId();
            var metodeExterne = new MetodeExterne(context);
            AdaugaProdusViewModel model = new AdaugaProdusViewModel();
            var produs = metodeExterne.ListaProduse(userId,categorie, produsId: id);

            if(produs is Carcasa carcasa)
            {
                model.Carcasa = carcasa;
                model.Produs = carcasa.Produs;
            }
            if (produs is PastaCPU pasta)
            {
                model.Pasta = pasta;
                model.Produs = pasta.Produs;
            }
            if (produs is Motherboard placaDeBaza)
            {
                model.PlacaDeBaza = placaDeBaza;
                model.Produs = placaDeBaza.Produs;
            }
            if (produs is GPU placaVideo)
            {
                model.PlacaVideo = placaVideo;
                model.Produs = placaVideo.Produs;
            }
            if (produs is PlacutaRAM placutaRAM)
            {
                model.PlacutaRAM = placutaRAM;
                model.Produs = placutaRAM.Produs;
            }
            if (produs is CPU procesor)
            {
                model.Procesor = procesor;
                model.Produs = procesor.Produs;
            }
            if (produs is PSU sursa)
            {
                model.Sursa = sursa;
                model.Produs = sursa.Produs;
            }
            if (produs is UnitatiDeStocare stocare)
            {
                model.Stocare = stocare;
                model.Produs = stocare.Produs;
            }

            if (produs == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Salvare(AdaugaProdusViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var brand = context.branduri.Single(b => b.User.Id == userId);
            var categorie = context.categorie.Single(c => c.Id == model.Produs.CategorieId);
            var produs = context.produse.SingleOrDefault(p => p.IdProdus == model.Produs.IdProdus);
            model.Produs.Categorie = categorie;
            var metodeExterne = new MetodeExterne(context);
            //verificam daca actiunea reprezinta adaugarea unui produs nou sau modificarea unuia existent
            if(model.Produs.IdProdus != 0)
            {
                TempData["Mesaj"] = "ProdusModificat";
            }
            else
            {
                TempData["Mesaj"] = "ProdusAdaugat";
            }
            if (model.PlacaDeBaza != null)
            {
                if (model.Produs.IdProdus != 0)
                {
                    var placaDeBaza = context.placiDeBaza.SingleOrDefault(p => p.Id == model.PlacaDeBaza.Id);
                    metodeExterne.Asociere<Motherboard>(produs, placaDeBaza, model);
                }
                else
                {
                    model.PlacaDeBaza.Produs = model.Produs;
                    metodeExterne.AdaugareProdus(placaBaza: model.PlacaDeBaza);
                    model.Produs.BrandId = brand.Id;
                    context.produse.Add(model.Produs);
                }
            }
            else if (model.Carcasa != null)
            {
                if (model.Produs.IdProdus != 0)
                {
                    var carcasa = context.carcase.SingleOrDefault(c => c.Id == model.Carcasa.Id);
                    metodeExterne.Asociere<Carcasa>(produs, carcasa, model);
                }
                else
                {
                    model.Carcasa.Produs = model.Produs;
                    metodeExterne.AdaugareProdus(carcasa: model.Carcasa);
                    model.Produs.BrandId = brand.Id;
                    context.produse.Add(model.Produs);
                }
            }
            else if (model.Procesor != null)
            {
                if (model.Produs.IdProdus != 0)
                {
                    var procesor = context.procesoare.SingleOrDefault(p => p.Id == model.Procesor.Id);
                    metodeExterne.Asociere<CPU>(produs, procesor, model);
                }
                else
                {
                    model.Procesor.Produs = model.Produs;
                    metodeExterne.AdaugareProdus(procesor: model.Procesor);
                    model.Produs.BrandId = brand.Id;
                    context.produse.Add(model.Produs);
                }

            }
            else if (model.Pasta != null)
            {
                if (model.Produs.IdProdus != 0)
                {
                    var pasta = context.pasteProcesor.SingleOrDefault(p => p.Id == model.Pasta.Id);
                    metodeExterne.Asociere<PastaCPU>(produs, pasta, model);
                }
                else
                {
                    model.Pasta.Produs = model.Produs;
                    metodeExterne.AdaugareProdus(pastaCpu: model.Pasta);
                    model.Produs.BrandId = brand.Id;
                    context.produse.Add(model.Produs);
                }
            }
            else if (model.PlacaVideo != null)
            {
                if (model.Produs.IdProdus != 0)
                {
                    var placa = context.placiVideo.SingleOrDefault(p => p.Id == model.PlacaVideo.Id);
                    metodeExterne.Asociere<GPU>(produs, placa, model);
                }
                else
                {
                    model.PlacaVideo.Produs = model.Produs;
                    metodeExterne.AdaugareProdus(placaVideo: model.PlacaVideo);
                    model.Produs.BrandId = brand.Id;
                    context.produse.Add(model.Produs);
                }
            }
            else if (model.PlacutaRAM != null)
            {
                if (model.Produs.IdProdus != 0)
                {
                    var placa = context.placuteRAM.SingleOrDefault(p => p.Id == model.PlacutaRAM.Id);
                    metodeExterne.Asociere<PlacutaRAM>(produs, placa, model);
                }
                else
                {
                    model.PlacutaRAM.Produs = model.Produs;
                    metodeExterne.AdaugareProdus(placuteRAM: model.PlacutaRAM);
                    model.Produs.BrandId = brand.Id;
                    context.produse.Add(model.Produs);
                }

            }
            else if (model.Sursa != null)
            {
                if (model.Produs.IdProdus != 0)
                {
                    var sursa = context.surse.SingleOrDefault(s => s.Id == model.Sursa.Id);
                    metodeExterne.Asociere<PSU>(produs, sursa, model);
                }
                else
                {
                    model.Sursa.Produs = model.Produs;
                    metodeExterne.AdaugareProdus(sursa: model.Sursa);
                    model.Produs.BrandId = brand.Id;
                    context.produse.Add(model.Produs);
                }
            }
            else if (model.Stocare != null)
            {
                if (model.Produs.IdProdus != 0)
                {
                    var stocare = context.stocare.SingleOrDefault(s => s.Id == model.Stocare.Id);
                    metodeExterne.Asociere<UnitatiDeStocare>(produs, stocare, model);
                }
                else
                {
                    model.Stocare.Produs = model.Produs;
                    metodeExterne.AdaugareProdus(stocare: model.Stocare);
                    model.Produs.BrandId = brand.Id;
                    context.produse.Add(model.Produs);
                }
            }
                context.SaveChanges();
           
            return RedirectToAction("Index", new {categorie = model.Produs.Categorie.Nume});
        }

           
        
        public ActionResult ViewAdauga(string categorie, int? idProdus )
        {
            AdaugaProdusViewModel model = new AdaugaProdusViewModel();
            model.Produs = new Produs();
            var produs = new Produs();
            switch (categorie)
            {
                
                case "Carcase":
                    /*
                     daca parametrul idProdus are o valoare atunci vom transmite 
                     un model cu informatii existente in baza de date
                     */
                    if (idProdus.HasValue)
                    {
                        produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                        var carcasa = context.carcase.Include(c =>c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus); ;
                        model.Produs = carcasa.Produs;
                        model.Produs.CategorieId = carcasa.Produs.CategorieId;
                        model.Carcasa = carcasa;
                        return View(model);
                    }
                    model.Carcasa = new Carcasa();
                    model.Produs.CategorieId = 1;
                    break;

                case "PlaciDeBaza":
                    if (idProdus.HasValue)
                    {
                        produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                        var placaDeBaza = context.placiDeBaza.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        model.Produs = produs;
                        model.PlacaDeBaza = placaDeBaza;
                        return View(model);
                    }
                    model.PlacaDeBaza = new Motherboard();
                    model.Produs.CategorieId = 2;
                    break;

                case "PlaciVideo":
                    if (idProdus.HasValue)
                    {
                        produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                        var placaVideo = context.placiVideo.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        model.Produs = produs;
                        model.PlacaVideo = placaVideo;
                        return View(model);
                    }
                    model.PlacaVideo = new GPU();
                    model.Produs.CategorieId = 3;
                    break;

                case "PlacuteRAM":
                    if (idProdus.HasValue)
                    {
                        produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                        var placutaRAM = context.placuteRAM.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        model.Produs = produs;
                        model.PlacutaRAM = placutaRAM;
                        return View(model);
                    }
                    model.PlacutaRAM = new PlacutaRAM();
                    model.Produs.CategorieId = 4;
                    break;

                case "Procesoare":
                    if (idProdus.HasValue)
                    {
                        produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                        var procesor = context.procesoare.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        model.Produs = produs;
                        model.Procesor = procesor;
                        return View(model);
                    }
                    model.Procesor = new CPU();
                    model.Produs.CategorieId = 5;
                    break;

                case "SurseDeAlimentare":
                    if (idProdus.HasValue)
                    {
                        produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                        var sursa = context.surse.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        model.Produs = produs;
                        model.Sursa = sursa;
                        return View(model);
                    }
                    model.Sursa = new PSU();
                    model.Produs.CategorieId = 6;
                    break;

                case "UnitatiStocare":
                    if (idProdus.HasValue)
                    {
                        produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                        var stocare = context.stocare.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        model.Produs = produs;
                        model.Stocare = stocare;
                        return View(model);
                    }
                    model.Stocare = new UnitatiDeStocare();
                    model.Produs.CategorieId = 7;
                    break;
          
                case "PastaProcesor":
                    if (idProdus.HasValue)
                    {
                        produs = context.produse.FirstOrDefault(p => p.IdProdus == idProdus);
                        var pastaCPU = context.pasteProcesor.Include(c => c.Produs).SingleOrDefault(c => c.ProdusId == produs.IdProdus);
                        model.Produs = produs;
                        model.Pasta = pastaCPU;
                        return View(model);
                    }
                    model.Pasta = new PastaCPU();
                    model.Produs.CategorieId = 8;
                    break;
            }
            return View(model);
        }
        public ActionResult Sterge(int id)
        {
            TempData["Mesaj"] = "ProdusSters";
            var produs = context.produse.Include(p => p.Categorie).Single(c => c.IdProdus == id);
            if(produs == null)
            {
                return HttpNotFound();
            }
            var categorie = produs.Categorie.Nume;
            var cos = context.cos.Where(c =>c.ProdusId == produs.IdProdus).ToList();
            var listaFavorite = context.produseFavorite.Where(c =>c.ProdusId == produs.IdProdus).ToList();
            //odata ce stergem un produs,acesta va fi sters din cosul si lista favorita a tuturor 
            foreach (var c in cos)
            {
                context.cos.Remove(c);
            }
            foreach(var c in listaFavorite)
            {
                context.produseFavorite.Remove(c);
            }
            context.produse.Remove(produs);
            context.SaveChanges();
            return RedirectToAction("Index",new { categorie = categorie });
        }

    }
}