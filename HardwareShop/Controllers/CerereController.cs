using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace HardwareShop.Controllers
{
    public class CerereController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Cerere
        public ActionResult Index(string tipCerere)
        {
            string userId = User.Identity.GetUserId();
            Cerere cerere;
            if (tipCerere == "Furnizor nou")
            {
                cerere = new Cerere();
            }
            else
            {
                cerere = context.cereri.SingleOrDefault(c => c.Utilizator.Id == userId && c.Status == "Acceptata");
            }
            cerere.tipCerere = tipCerere;
            return View(cerere);
        }

        public ActionResult Salvare(Cerere cerere)
        {

            string userId = User.Identity.GetUserId();
            ApplicationUser utilizatorAutentificat = context.Users.Find(userId);
            var cerereExistenta = context.cereri.SingleOrDefault(c => c.Utilizator.Id == userId);
            if (cerereExistenta == null)
            {
                cerere.tipCerere = "Furnizor nou";
            }
            else
            {
                cerere.Oras = cerereExistenta.Oras;
                cerere.CUI = cerereExistenta.CUI;
                cerere.Brand = cerereExistenta.Brand;
                cerere.tipCerere = "Modificare produse";
            }
            cerere.Utilizator = utilizatorAutentificat;
            cerere.Utilizator.UserName = User.Identity.GetUserName();
            cerere.Status = "Nerezolvata";


            context.cereri.Add(cerere);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ListaCereri()
        {
            var cereri = context.cereri.Include(c => c.Utilizator).Where(c => c.Status == "Nerezolvata").ToList();
            return View(cereri);
        }


        public ActionResult Raspuns(int cerereId, bool cerereAcceptata, string userId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            var cerereNoua = context.cereri.Include(c => c.Utilizator).FirstOrDefault(c => c.Id == cerereId);

            var cerereExistenta = context.cereri.Where(c => c.Utilizator.Id == user.Id).ToList()[0];
            if (cerereAcceptata == true)
            {
                if (cerereNoua.tipCerere == "Furnizor nou")
                {
                    StergeRol("Client", user);
                    AlocaRol(userId, "Furnizor");
                    BrandUser brandUser = new BrandUser()
                    {
                        numeBrand = cerereNoua.Brand,
                        User = user
                    };
                    context.branduri.Add(brandUser);
                }
                else
                {
                    context.cereri.Remove(cerereExistenta);
                }

                cerereNoua.Status = "Acceptata";
            }
            else
            {
                context.cereri.Remove(cerereNoua);
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public void StergeRol(string roleName, ApplicationUser user)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var role = roleManager.FindByName(roleName);
            if (role != null && user != null)
            {
                var userRole = context.Set<IdentityUserRole>().FirstOrDefault(ur => ur.RoleId == role.Id && ur.UserId == user.Id);

                context.Set<IdentityUserRole>().Remove(userRole);
                context.SaveChanges();
            }
        }

        public void AlocaRol(string userId, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!userManager.IsInRole(userId, roleName))
            {
                userManager.AddToRole(userId, roleName);
            }
        }
    }
}