using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HardwareShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BrandUser> branduri { get; set; }
        public DbSet<Carcasa> carcase { get; set; }
        public DbSet<Cerere> cereri { get; set; }
        public DbSet<CosCumparaturi> cos { get; set; }
        public DbSet<CPU> procesoare { get; set; }
        public DbSet<GPU> placiVideo { get; set; }
        public DbSet<Motherboard> placiDeBaza { get; set; }
        public DbSet<PastaCPU> pasteProcesor { get; set; }
        public DbSet<PlacutaRAM> placuteRAM { get; set; }
        public DbSet<ProdusFavorit> produseFavorite { get; set; }
        public DbSet<PSU> surse { get; set; }
        public DbSet<UnitatiDeStocare> stocare { get; set; }
        public DbSet<Comanda> comanda { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Produs>();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}