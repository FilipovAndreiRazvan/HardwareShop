using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class CerereController : Controller
    {
        private readonly ApplicationDbContext context;

        public CerereController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Afișează formularul pentru trimiterea unei cereri de transformare în furnizor
        public ActionResult Index()
        {
            Cerere cerere = new Cerere();
            return View(cerere);
        }

        // Salvează cererea completată de utilizator în baza de date
        public async Task<ActionResult> Salvare(Cerere cerere)
        {
            string userId = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                // Dacă utilizatorul nu este autentificat
                return new HttpStatusCodeResult(400, "UserId lipsă");
            }

            // Se completează cererea cu informațiile utilizatorului și statusul inițial
            cerere.UtilizatorId = userId;
            cerere.Status = "Nerezolvata";
            context.cereri.Add(cerere);

            await context.SaveChangesAsync();

            // După salvare, redirecționează către pagina principală
            return RedirectToAction("Index", "Home");
        }

        // Afișează lista cererilor nerezolvate pentru administratori
        public async Task<ActionResult> ListaCereri()
        {
            var cereri = await context.cereri
                .Include(c => c.Utilizator)
                .Where(c => c.Status == "Nerezolvata")
                .ToListAsync();

            return View(cereri);
        }

        // Răspunde la o cerere: fie o acceptă și transformă utilizatorul în furnizor, fie o respinge
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Raspuns(int cerereId, bool cerereAcceptata, string userId)
        {
            // Se caută utilizatorul după ID
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return HttpNotFound("Utilizatorul nu a fost găsit.");
            }

            // Se caută cererea corespunzătoare
            var cerere = await context.cereri
                .Include(c => c.Utilizator)
                .FirstOrDefaultAsync(c => c.Id == cerereId);

            if (cerere == null)
            {
                throw new HttpException(404, "Cerere inexistenta!");
            }

            if (cerereAcceptata)
            {
                // Dacă cererea este acceptată: 
                // se schimbă rolul utilizatorului din Client în Furnizor
                await StergeRol("Client", userId);
                await AlocaRol(userId, "Furnizor");

                // Se adaugă brandul nou înregistrat în tabelul BrandUser
                BrandUser brandUser = new BrandUser()
                {
                    NumeBrand = cerere.Brand,
                    UserId = user.Id
                };
                context.branduri.Add(brandUser);

                // Se actualizează statusul cererii
                cerere.Status = "Acceptata";
            }
            else
            {
                // Dacă cererea este respinsă, se șterge din baza de date
                context.cereri.Remove(cerere);
            }

            await context.SaveChangesAsync();

            // Se revine la lista de cereri
            return RedirectToAction("ListaCereri");
        }

        // Elimină un rol din lista de roluri ale unui utilizator
        public async Task StergeRol(string roleName, string userId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (await userManager.IsInRoleAsync(userId, roleName))
            {
                await userManager.RemoveFromRoleAsync(userId, roleName);
            }
        }

        // Atribuie un nou rol utilizatorului (dacă nu îl are deja)
        public async Task AlocaRol(string userId, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!userManager.IsInRole(userId, roleName))
            {
                await userManager.AddToRoleAsync(userId, roleName);
            }
        }
    }
}