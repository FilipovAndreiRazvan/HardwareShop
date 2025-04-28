using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Reflection.Emit;
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
        public DbSet<Factura> factura { get; set; }
        public DbSet<ProdusComandat> produseComandate { get; set; }
        public DbSet<Card> carduri { get; set; }
        public DbSet<Categorie> categorie { get; set; }
        public DbSet<Produs> produse { get; set; }
        public DbSet<AdresaLivrare> adrese { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produs>().ToTable("Produse");
            modelBuilder.Entity<Carcasa>().ToTable("Carcase");
            modelBuilder.Entity<CPU>().ToTable("Procesoare");
            modelBuilder.Entity<GPU>().ToTable("PlaciVideo");
            modelBuilder.Entity<Motherboard>().ToTable("PlaciDeBaza");
            modelBuilder.Entity<PastaCPU>().ToTable("PastaProcesor");
            modelBuilder.Entity<PlacutaRAM>().ToTable("PlacuteRAM");
            modelBuilder.Entity<PSU>().ToTable("Surse");
            modelBuilder.Entity<UnitatiDeStocare>().ToTable("Stocare");
            modelBuilder.Entity<BrandUser>().ToTable("Branduri");
            modelBuilder.Entity<Cerere>().ToTable("Cereri");
            modelBuilder.Entity<CosCumparaturi>().ToTable("CosCumparaturi");
            modelBuilder.Entity<ProdusFavorit>().ToTable("ProduseFavorite");
            modelBuilder.Entity<Comanda>().ToTable("Comenzi");
            modelBuilder.Entity<Factura>().ToTable("Facturi");
            modelBuilder.Entity<ProdusComandat>().ToTable("ProduseComandate");
            modelBuilder.Entity<Categorie>().ToTable("Categorii");
            modelBuilder.Entity<Card>().ToTable("Carduri");
            modelBuilder.Entity<AdresaLivrare>().ToTable("AdreseLivrare");
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}