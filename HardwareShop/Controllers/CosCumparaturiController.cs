using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System;
using System.Threading.Tasks;
using System.Web;
namespace HardwareShop.Controllers
{
    public class CosCumparaturiController : Controller
    {
        private readonly ApplicationDbContext context;

        // Constructor care primește contextul bazei de date
        public CosCumparaturiController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Afișează produsele din coșul de cumpărături ale utilizatorului logat
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();

            // Se extrag din DB toate produsele din coș ale utilizatorului curent,
            // inclusiv informațiile despre produs, categorie și brand
            var cos = await context.cos
                .Include(c => c.Produs)
                .Include(c => c.Produs.Categorie)
                .Include(c => c.Produs.Brand)
                .Where(c => c.ClientId == userId)
                .ToListAsync();

            return View(cos);
        }

        // Adaugă un produs în coș sau crește cantitatea dacă produsul este deja în coș
        public async Task<ActionResult> Adauga(int ProdusId, int nrBuc, string viewName, bool paginaOferte = false)
        {
            var userId = User.Identity.GetUserId();

            // Verifică dacă produsul e deja în coșul utilizatorului
            var cosDb = await context.cos
                .SingleOrDefaultAsync(c => c.ProdusId == ProdusId && c.ClientId == userId);

            // Caută produsul după ID, inclusiv categoria pentru redirect ulterior
            var produs = await context.produse
                .Include(p => p.Categorie)
                .SingleOrDefaultAsync(p => p.IdProdus == ProdusId);

            // Verificări de validitate: număr de bucăți pozitiv și produs existent + stoc suficient
            if (nrBuc <= 0)
            {
                throw new HttpException(400, "Număr de bucăți invalid");
            }

            if (produs == null || produs.Stoc < nrBuc)
            {
                return HttpNotFound("Produs inexistent sau stoc insuficient");
            }

            // Scade stocul produsului
            produs.Stoc -= nrBuc;

            if (cosDb != null)
            {
                // Dacă produsul e deja în coș, doar actualizează cantitatea
                cosDb.NrBuc += nrBuc;
            }
            else
            {
                // Dacă nu există în coș, creează o nouă intrare
                var cos = new CosCumparaturi
                {
                    ProdusId = ProdusId,
                    ClientId = userId,
                    NrBuc = nrBuc
                };
                context.cos.Add(cos);
            }

            await context.SaveChangesAsync();

            // Mesaj temporar pentru confirmare
            TempData["Mesaj"] = "ProdusAdauagatCos";

            // Redirect în funcție de sursa de unde a fost adăugat produsul
            if (viewName == "cosCumparaturi")
            {
                return RedirectToAction("Index", "CosCumparaturi");
            }
            else if (viewName == "listaFavorite")
            {
                return RedirectToAction("Index", "Listafavorite");
            }
            else if (viewName == "vizualizare")
            {
                return RedirectToAction("Vizualizare", "Servicii", new { id = produs.IdProdus, categorie = produs.Categorie.Nume });
            }
            else
            {
                return RedirectToAction("Index", "Servicii", new { categorie = produs.Categorie.Nume, paginaOferte = paginaOferte });
            }
        }

        // Elimină un produs sau o anumită cantitate din coșul de cumpărături
        public async Task<ActionResult> Sterge(int ProdusId, int nrBuc)
        {
            var userId = User.Identity.GetUserId();

            // Caută produsul din coș pentru utilizator
            var cos = await context.cos
                .SingleOrDefaultAsync(c => c.ProdusId == ProdusId && c.ClientId == userId);

            // Caută produsul pentru a actualiza stocul
            var produs = await context.produse
                .SingleOrDefaultAsync(c => c.IdProdus == ProdusId);

            // Verificări de validitate
            if (cos == null)
            {
                throw new HttpException(404, "Cos gol");
            }
            if (produs == null)
            {
                throw new HttpException(404, "Produs inexistent!");
            }
            if (nrBuc <= 0)
            {
                throw new HttpException(400, "Număr de bucăți invalid");
            }

            // Reface stocul produsului
            produs.Stoc += nrBuc;

            // Scade cantitatea din coș
            cos.NrBuc -= nrBuc;

            // Dacă nu mai rămân bucăți, se șterge produsul din coș
            if (cos.NrBuc == 0)
            {
                context.cos.Remove(cos);
            }

            await context.SaveChangesAsync();

            // Redirect înapoi către coș
            return RedirectToAction("Index", "CosCumparaturi");
        }
    }
}
