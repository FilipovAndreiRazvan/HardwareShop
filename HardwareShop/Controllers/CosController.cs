using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System;

namespace HardwareShop.Controllers
{
    public class CosController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Cos

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();


            // PAS 1: aduci datele în memorie
            var cos = context.cos.Include(c => c.Produs).Include(c => c.Produs.Categorie).Include(c => c.Produs.Brand).Where(c =>c.ClientId == userId).ToList();

            return View(cos);
        }
        public ActionResult Adauga(int ProdusId, int nrBuc, string viewName,bool paginaOferte = false)
        {
            var userId = User.Identity.GetUserId();
            var cosDb = context.cos.SingleOrDefault(c => c.ProdusId == ProdusId && c.ClientId == userId);
            var produs = context.produse.Include(p => p.Categorie).SingleOrDefault(p => p.IdProdus == ProdusId);
            context.produse.SingleOrDefault(p => p.IdProdus == ProdusId).Stoc -= nrBuc;
            if (cosDb != null)
            {
                cosDb.NrBuc += nrBuc;
            }
            else
            {
                var cos = new CosCumparaturi();
                cos.ProdusId = ProdusId;
                cos.ClientId = User.Identity.GetUserId();
                cos.NrBuc = nrBuc;
                context.cos.Add(cos);
            }
            context.SaveChanges();
            if (viewName == "cosCumparaturi")
            {
                return RedirectToAction("Index", "Cos");
            }
            else if(viewName == "listaFavorite")
            {
                TempData["Mesaj"] = "ProdusAdauagatCos";
                return RedirectToAction("Index","Listafavorite");
            }
            else
            {
                TempData["Mesaj"] = "ProdusAdauagatCos";
                return RedirectToAction("Index", "Servicii", new { categorie = produs.Categorie.Nume, paginaOferte = paginaOferte });
            }
        }

        public ActionResult Sterge(int ProdusId, int nrBuc)
        {
            var userId = User.Identity.GetUserId();
            CosCumparaturi cos;
            if (userId == null)
            {
                cos = context.cos.FirstOrDefault(c => c.ProdusId == ProdusId && c.ClientId == userId);
            }
            else
            {
                cos = context.cos.FirstOrDefault(c => c.ProdusId == ProdusId && c.ClientId == userId);
            }

            context.produse.SingleOrDefault(c => c.IdProdus == ProdusId).Stoc += nrBuc;
            cos.NrBuc -= nrBuc;
            if (cos.NrBuc == 0)
            {
                context.cos.Remove(cos);
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Cos");
        }

    }
}