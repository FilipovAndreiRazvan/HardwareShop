using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
            Cerere cerere = new Cerere();
            return View(cerere);
        }

        public ActionResult Salvare(Cerere cerere)
        {

            string userId = User.Identity.GetUserId();
            ApplicationUser utilizatorAutentificat = context.Users.Find(userId);
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
            if (cerereAcceptata == true)
            {
                    StergeRol("Client", userId);
                    AlocaRol(userId, "Furnizor");
                    BrandUser brandUser = new BrandUser()
                    {
                        NumeBrand = cerereNoua.Brand,
                        UserId = user.Id
                    };
                    context.branduri.Add(brandUser);
                cerereNoua.Status = "Acceptata";
            }
            else
            {
                context.cereri.Remove(cerereNoua);
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Cerere");
        }


        public void StergeRol(string roleName, string userId)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var role = roleManager.FindByName(roleName);
            if (role != null && !string.IsNullOrEmpty(userId))
            {
                var userRole = context.Set<IdentityUserRole>()
                    .FirstOrDefault(ur => ur.RoleId == role.Id && ur.UserId == userId);

                if (userRole != null)
                {
                    context.Set<IdentityUserRole>().Remove(userRole);
                    context.SaveChanges();
                }
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