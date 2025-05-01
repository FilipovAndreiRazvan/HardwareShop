using HardwareShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace HardwareShop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Afișează informațiile utilizatorului logat (Contul Clientului)
        public ActionResult ContClient()
        {
            string currentUserName = User.Identity.Name;

            // Caută utilizatorul după numele de utilizator (UserName)
            ApplicationUser user = context.Users.SingleOrDefault(u => u.UserName == currentUserName);

            if (user == null)
            {
                return HttpNotFound("Utilizatorul nu a fost găsit.");
            }

            return View(user);
        }

        // Afișează produsele aflate la ofertă (pentru client sau furnizor)
        public async Task<ActionResult> Index(bool paginaOferte = false)
        {
            // Dacă utilizatorul a venit de pe pagina de oferte, ștergem mesajul temporar
            if (paginaOferte == true)
            {
                TempData["Mesaj"] = null;
            }

            var userId = User.Identity.GetUserId();
            List<Produs> oferte;

            if (User.IsInRole("Furnizor"))
            {
                // Dacă utilizatorul este furnizor, se vor afișa doar produsele aflate la ofertă ale brandului său
                var brand = await context.branduri.SingleOrDefaultAsync(b => b.UserId == userId);
                oferte = await context.produse
                    .Include(p => p.Categorie)
                    .Where(p => p.Oferta != null && p.BrandId == brand.Id)
                    .ToListAsync();
            }
            else
            {
                // Dacă este client, se vor afișa toate produsele aflate la ofertă
                oferte = await context.produse
                    .Include(p => p.Categorie)
                    .Where(p => p.Oferta != null)
                    .ToListAsync();
            }

            // Obține lista ID-urilor produselor favorite ale utilizatorului curent
            var favoriteIds = await context.produseFavorite
                .Where(p => p.UtilizatorId == userId)
                .Select(p => p.ProdusId)
                .ToListAsync();

            // Marchează în listă produsele ca fiind favorite sau nu
            foreach (Produs produs in oferte)
            {
                produs.ProdusFavorit = favoriteIds.Contains(produs.IdProdus);
            }

            return View(oferte);
        }
    }
}
