namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdreseLivrare",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Oras = c.String(nullable: false),
                        Localitate = c.String(nullable: false),
                        Strada = c.String(nullable: false),
                        Bloc = c.String(),
                        Numar = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branduri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        NumeBrand = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nume = c.String(),
                        Prenume = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Carcase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        Format = c.String(nullable: false),
                        NrVentilatoare = c.Int(nullable: false),
                        DeschidereLaterala = c.Boolean(nullable: false),
                        TipCarcasa = c.String(nullable: false),
                        NrLocaseSloturiExtensii = c.Int(nullable: false),
                        Dimensiune = c.String(nullable: false),
                        Greutate = c.Single(nullable: false),
                        Culoare = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .Index(t => t.ProdusId);
            
            CreateTable(
                "dbo.Produse",
                c => new
                    {
                        IdProdus = c.Int(nullable: false, identity: true),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        ImgLink = c.String(nullable: false),
                        Stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        CategorieId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.IdProdus)
                .ForeignKey("dbo.Categorii", t => t.CategorieId, cascadeDelete: true)
                .Index(t => t.CategorieId);
            
            CreateTable(
                "dbo.Categorii",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Nume = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carduri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeDetinator = c.String(nullable: false),
                        NrCard = c.String(nullable: false),
                        CVC = c.Int(nullable: false),
                        Sold = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cereri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UtilizatorId = c.String(maxLength: 128),
                        CUI = c.String(nullable: false),
                        Brand = c.String(nullable: false),
                        Oras = c.String(nullable: false),
                        Carcase = c.Boolean(nullable: false),
                        PlaciDeBaza = c.Boolean(nullable: false),
                        Procesoare = c.Boolean(nullable: false),
                        PlaciVideo = c.Boolean(nullable: false),
                        PlacuteRAM = c.Boolean(nullable: false),
                        UnitatiDeStocare = c.Boolean(nullable: false),
                        Surse = c.Boolean(nullable: false),
                        PastaProcesor = c.Boolean(nullable: false),
                        Status = c.String(),
                        TipCerere = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UtilizatorId)
                .Index(t => t.UtilizatorId);
            
            CreateTable(
                "dbo.Comenzi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(maxLength: 128),
                        AdresaId = c.Int(nullable: false),
                        FacturaId = c.Int(nullable: false),
                        PretComanda = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdreseLivrare", t => t.AdresaId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .ForeignKey("dbo.Facturi", t => t.FacturaId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.AdresaId)
                .Index(t => t.FacturaId);
            
            CreateTable(
                "dbo.Facturi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nume = c.String(nullable: false),
                        Prenume = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        NrTelefon = c.Int(nullable: false),
                        AdresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdreseLivrare", t => t.AdresaId)
                .Index(t => t.AdresaId);
            
            CreateTable(
                "dbo.CosCumparaturi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategorieId = c.Byte(nullable: false),
                        ProdusId = c.Int(nullable: false),
                        NrBuc = c.Int(nullable: false),
                        ClientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorii", t => t.CategorieId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .ForeignKey("dbo.Produse", t => t.ProdusId)
                .Index(t => t.CategorieId)
                .Index(t => t.ProdusId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.PastaProcesor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        ConductivitateTermica = c.String(nullable: false),
                        RezistentaTermica = c.String(nullable: false),
                        Cantitate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .Index(t => t.ProdusId);
            
            CreateTable(
                "dbo.PlaciDeBaza",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        Socket = c.String(nullable: false),
                        Chipset = c.String(nullable: false),
                        NrSloturiRAM = c.Int(nullable: false),
                        TipRAM = c.String(nullable: false),
                        SloturiPCIe = c.String(nullable: false),
                        ConectoriSATA = c.String(nullable: false),
                        ConectoriM2 = c.String(nullable: false),
                        TipPortUSB = c.String(nullable: false),
                        NrPorturiUSB = c.Int(nullable: false),
                        FormFactor = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .Index(t => t.ProdusId);
            
            CreateTable(
                "dbo.PlaciVideo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        CapacitateMemorie = c.Int(nullable: false),
                        FrecventaMemorie = c.Int(nullable: false),
                        RezolutieMaxima = c.String(nullable: false),
                        SistemRacire = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .Index(t => t.ProdusId);
            
            CreateTable(
                "dbo.PlacuteRAM",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        Tip = c.String(nullable: false),
                        Capacitate = c.Int(nullable: false),
                        Frecventa = c.Int(nullable: false),
                        Module = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .Index(t => t.ProdusId);
            
            CreateTable(
                "dbo.Procesoare",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        FrecventaBaza = c.Single(nullable: false),
                        FrecventaTurbo = c.Single(nullable: false),
                        NrNuclee = c.Int(nullable: false),
                        NrThreads = c.Int(nullable: false),
                        PutereTermica = c.String(nullable: false),
                        Socket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .Index(t => t.ProdusId);
            
            CreateTable(
                "dbo.ProduseComandate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComandaId = c.Int(nullable: false),
                        ProdusId = c.Int(nullable: false),
                        CategorieId = c.Byte(nullable: false),
                        Cantitate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorii", t => t.CategorieId, cascadeDelete: true)
                .ForeignKey("dbo.Comenzi", t => t.ComandaId, cascadeDelete: true)
                .ForeignKey("dbo.Produse", t => t.ProdusId)
                .Index(t => t.ComandaId)
                .Index(t => t.ProdusId)
                .Index(t => t.CategorieId);
            
            CreateTable(
                "dbo.ProduseFavorite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        UtilizatorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UtilizatorId)
                .Index(t => t.ProdusId)
                .Index(t => t.UtilizatorId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Stocare",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        FormFactor = c.String(nullable: false),
                        Capacitate = c.Int(nullable: false),
                        TipMemorie = c.String(nullable: false),
                        RataTransferCitire = c.Int(nullable: false),
                        RataTransferScriere = c.Int(nullable: false),
                        Dimensiune = c.String(nullable: false),
                        Greutate = c.Single(nullable: false),
                        LineUp = c.String(nullable: false),
                        TipController = c.String(nullable: false),
                        Interfata = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .Index(t => t.ProdusId);
            
            CreateTable(
                "dbo.Surse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        Putere = c.Int(nullable: false),
                        NrVentilatoare = c.Int(nullable: false),
                        Alimentare = c.String(nullable: false),
                        Format = c.String(nullable: false),
                        Greutate = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produse", t => t.ProdusId, cascadeDelete: true)
                .Index(t => t.ProdusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surse", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.Stocare", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProduseFavorite", "UtilizatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProduseFavorite", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.ProduseComandate", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.ProduseComandate", "ComandaId", "dbo.Comenzi");
            DropForeignKey("dbo.ProduseComandate", "CategorieId", "dbo.Categorii");
            DropForeignKey("dbo.Procesoare", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.PlacuteRAM", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.PlaciVideo", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.PlaciDeBaza", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.PastaProcesor", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.CosCumparaturi", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.CosCumparaturi", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CosCumparaturi", "CategorieId", "dbo.Categorii");
            DropForeignKey("dbo.Comenzi", "FacturaId", "dbo.Facturi");
            DropForeignKey("dbo.Facturi", "AdresaId", "dbo.AdreseLivrare");
            DropForeignKey("dbo.Comenzi", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comenzi", "AdresaId", "dbo.AdreseLivrare");
            DropForeignKey("dbo.Cereri", "UtilizatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Carcase", "ProdusId", "dbo.Produse");
            DropForeignKey("dbo.Produse", "CategorieId", "dbo.Categorii");
            DropForeignKey("dbo.Branduri", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Surse", new[] { "ProdusId" });
            DropIndex("dbo.Stocare", new[] { "ProdusId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProduseFavorite", new[] { "UtilizatorId" });
            DropIndex("dbo.ProduseFavorite", new[] { "ProdusId" });
            DropIndex("dbo.ProduseComandate", new[] { "CategorieId" });
            DropIndex("dbo.ProduseComandate", new[] { "ProdusId" });
            DropIndex("dbo.ProduseComandate", new[] { "ComandaId" });
            DropIndex("dbo.Procesoare", new[] { "ProdusId" });
            DropIndex("dbo.PlacuteRAM", new[] { "ProdusId" });
            DropIndex("dbo.PlaciVideo", new[] { "ProdusId" });
            DropIndex("dbo.PlaciDeBaza", new[] { "ProdusId" });
            DropIndex("dbo.PastaProcesor", new[] { "ProdusId" });
            DropIndex("dbo.CosCumparaturi", new[] { "ClientId" });
            DropIndex("dbo.CosCumparaturi", new[] { "ProdusId" });
            DropIndex("dbo.CosCumparaturi", new[] { "CategorieId" });
            DropIndex("dbo.Facturi", new[] { "AdresaId" });
            DropIndex("dbo.Comenzi", new[] { "FacturaId" });
            DropIndex("dbo.Comenzi", new[] { "AdresaId" });
            DropIndex("dbo.Comenzi", new[] { "ClientId" });
            DropIndex("dbo.Cereri", new[] { "UtilizatorId" });
            DropIndex("dbo.Produse", new[] { "CategorieId" });
            DropIndex("dbo.Carcase", new[] { "ProdusId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Branduri", new[] { "UserId" });
            DropTable("dbo.Surse");
            DropTable("dbo.Stocare");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProduseFavorite");
            DropTable("dbo.ProduseComandate");
            DropTable("dbo.Procesoare");
            DropTable("dbo.PlacuteRAM");
            DropTable("dbo.PlaciVideo");
            DropTable("dbo.PlaciDeBaza");
            DropTable("dbo.PastaProcesor");
            DropTable("dbo.CosCumparaturi");
            DropTable("dbo.Facturi");
            DropTable("dbo.Comenzi");
            DropTable("dbo.Cereri");
            DropTable("dbo.Carduri");
            DropTable("dbo.Categorii");
            DropTable("dbo.Produse");
            DropTable("dbo.Carcase");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Branduri");
            DropTable("dbo.AdreseLivrare");
        }
    }
}
