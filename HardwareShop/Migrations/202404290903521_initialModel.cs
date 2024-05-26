namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BrandUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        numeBrand = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
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
                "dbo.Carcasas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        format = c.String(nullable: false),
                        nrVentilatoare = c.Int(nullable: false),
                        deschidereLaterala = c.Boolean(nullable: false),
                        tipCarcasa = c.String(nullable: false),
                        nrLocaseSloturiExtensii = c.Int(nullable: false),
                        dimensiune = c.String(nullable: false),
                        greutate = c.Single(nullable: false),
                        culoare = c.String(nullable: false),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        imgLink = c.String(nullable: false),
                        stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        produsFavorit = c.Boolean(nullable: false),
                        categorie = c.String(),
                        CosCumparaturi_Id = c.Int(),
                        ProdusFavorit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CosCumparaturis", t => t.CosCumparaturi_Id)
                .ForeignKey("dbo.ProduseFavorites", t => t.ProdusFavorit_Id)
                .Index(t => t.CosCumparaturi_Id)
                .Index(t => t.ProdusFavorit_Id);
            
            CreateTable(
                "dbo.Cereres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CUI = c.String(nullable: false),
                        Brand = c.String(nullable: false),
                        Oras = c.String(nullable: false),
                        Carcase = c.Boolean(nullable: false),
                        placiDeBaza = c.Boolean(nullable: false),
                        procesoare = c.Boolean(nullable: false),
                        placiVideo = c.Boolean(nullable: false),
                        placuteRAM = c.Boolean(nullable: false),
                        unitatiDeStocare = c.Boolean(nullable: false),
                        surse = c.Boolean(nullable: false),
                        pastaProcesor = c.Boolean(nullable: false),
                        Status = c.String(),
                        Utilizator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Utilizator_Id)
                .Index(t => t.Utilizator_Id);
            
            CreateTable(
                "dbo.CosCumparaturis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        categorie = c.String(),
                        idProdus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GPUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CapacitateMemorie = c.Int(nullable: false),
                        FrecventaMemorie = c.Int(nullable: false),
                        RezolutieMaxima = c.String(nullable: false),
                        SistemRacire = c.String(nullable: false),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        imgLink = c.String(nullable: false),
                        stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        produsFavorit = c.Boolean(nullable: false),
                        categorie = c.String(),
                        CosCumparaturi_Id = c.Int(),
                        ProdusFavorit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CosCumparaturis", t => t.CosCumparaturi_Id)
                .ForeignKey("dbo.ProduseFavorites", t => t.ProdusFavorit_Id)
                .Index(t => t.CosCumparaturi_Id)
                .Index(t => t.ProdusFavorit_Id);
            
            CreateTable(
                "dbo.Motherboards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        socket = c.String(nullable: false),
                        chipset = c.String(nullable: false),
                        nrSloturiRAM = c.Int(nullable: false),
                        tipRAM = c.String(nullable: false),
                        sloturiPCIe = c.String(nullable: false),
                        conectoriSATA = c.String(nullable: false),
                        conectoriM2 = c.String(nullable: false),
                        tipPortUSB = c.String(nullable: false),
                        nrPorturiUSB = c.Int(nullable: false),
                        formFactor = c.String(nullable: false),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        imgLink = c.String(nullable: false),
                        stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        produsFavorit = c.Boolean(nullable: false),
                        categorie = c.String(),
                        CosCumparaturi_Id = c.Int(),
                        ProdusFavorit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CosCumparaturis", t => t.CosCumparaturi_Id)
                .ForeignKey("dbo.ProduseFavorites", t => t.ProdusFavorit_Id)
                .Index(t => t.CosCumparaturi_Id)
                .Index(t => t.ProdusFavorit_Id);
            
            CreateTable(
                "dbo.PastaCPUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        conductivitateTermica = c.String(),
                        rezistentaTermica = c.String(),
                        cantitate = c.Int(nullable: false),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        imgLink = c.String(nullable: false),
                        stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        produsFavorit = c.Boolean(nullable: false),
                        categorie = c.String(),
                        CosCumparaturi_Id = c.Int(),
                        ProdusFavorit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CosCumparaturis", t => t.CosCumparaturi_Id)
                .ForeignKey("dbo.ProduseFavorites", t => t.ProdusFavorit_Id)
                .Index(t => t.CosCumparaturi_Id)
                .Index(t => t.ProdusFavorit_Id);
            
            CreateTable(
                "dbo.PlacutaRAMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tip = c.String(nullable: false),
                        capacitate = c.Int(nullable: false),
                        frecventa = c.Int(nullable: false),
                        module = c.String(nullable: false),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        imgLink = c.String(nullable: false),
                        stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        produsFavorit = c.Boolean(nullable: false),
                        categorie = c.String(),
                        CosCumparaturi_Id = c.Int(),
                        ProdusFavorit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CosCumparaturis", t => t.CosCumparaturi_Id)
                .ForeignKey("dbo.ProduseFavorites", t => t.ProdusFavorit_Id)
                .Index(t => t.CosCumparaturi_Id)
                .Index(t => t.ProdusFavorit_Id);
            
            CreateTable(
                "dbo.PSUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        putere = c.Int(nullable: false),
                        nrVentilatoare = c.Int(nullable: false),
                        alimentare = c.String(),
                        format = c.String(),
                        greutate = c.Single(nullable: false),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        imgLink = c.String(nullable: false),
                        stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        produsFavorit = c.Boolean(nullable: false),
                        categorie = c.String(),
                        CosCumparaturi_Id = c.Int(),
                        ProdusFavorit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CosCumparaturis", t => t.CosCumparaturi_Id)
                .ForeignKey("dbo.ProduseFavorites", t => t.ProdusFavorit_Id)
                .Index(t => t.CosCumparaturi_Id)
                .Index(t => t.ProdusFavorit_Id);
            
            CreateTable(
                "dbo.UnitatiDeStocares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        formFactor = c.String(),
                        capacitate = c.Int(nullable: false),
                        tipMemorie = c.String(),
                        rataTransferCitire = c.Int(nullable: false),
                        rataTransferScriere = c.Int(nullable: false),
                        dimensiuni = c.String(),
                        greutate = c.Single(nullable: false),
                        lineUp = c.String(),
                        tipController = c.String(),
                        interfata = c.String(),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        imgLink = c.String(nullable: false),
                        stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        produsFavorit = c.Boolean(nullable: false),
                        categorie = c.String(),
                        CosCumparaturi_Id = c.Int(),
                        ProdusFavorit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CosCumparaturis", t => t.CosCumparaturi_Id)
                .ForeignKey("dbo.ProduseFavorites", t => t.ProdusFavorit_Id)
                .Index(t => t.CosCumparaturi_Id)
                .Index(t => t.ProdusFavorit_Id);
            
            CreateTable(
                "dbo.CPUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        frecventaBaza = c.Single(nullable: false),
                        frecventaTurbo = c.Single(nullable: false),
                        nrNuclee = c.Int(nullable: false),
                        nrThreads = c.Int(nullable: false),
                        putereTermica = c.String(nullable: false),
                        socket = c.String(nullable: false),
                        Nume = c.String(nullable: false),
                        Pret = c.Single(nullable: false),
                        Descriere = c.String(),
                        imgLink = c.String(nullable: false),
                        stoc = c.Int(nullable: false),
                        Brand = c.String(),
                        produsFavorit = c.Boolean(nullable: false),
                        categorie = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProduseFavorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        categorie = c.String(),
                        idProdus = c.Int(nullable: false),
                        Utilizator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Utilizator_Id)
                .Index(t => t.Utilizator_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProduseFavorites", "Utilizator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UnitatiDeStocares", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.PSUs", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.PlacutaRAMs", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.PastaCPUs", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.Motherboards", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.GPUs", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.Carcasas", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.UnitatiDeStocares", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropForeignKey("dbo.PSUs", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropForeignKey("dbo.PlacutaRAMs", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropForeignKey("dbo.PastaCPUs", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropForeignKey("dbo.Motherboards", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropForeignKey("dbo.GPUs", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropForeignKey("dbo.Carcasas", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropForeignKey("dbo.Cereres", "Utilizator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BrandUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProduseFavorites", new[] { "Utilizator_Id" });
            DropIndex("dbo.UnitatiDeStocares", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.UnitatiDeStocares", new[] { "CosCumparaturi_Id" });
            DropIndex("dbo.PSUs", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.PSUs", new[] { "CosCumparaturi_Id" });
            DropIndex("dbo.PlacutaRAMs", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.PlacutaRAMs", new[] { "CosCumparaturi_Id" });
            DropIndex("dbo.PastaCPUs", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.PastaCPUs", new[] { "CosCumparaturi_Id" });
            DropIndex("dbo.Motherboards", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.Motherboards", new[] { "CosCumparaturi_Id" });
            DropIndex("dbo.GPUs", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.GPUs", new[] { "CosCumparaturi_Id" });
            DropIndex("dbo.Cereres", new[] { "Utilizator_Id" });
            DropIndex("dbo.Carcasas", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.Carcasas", new[] { "CosCumparaturi_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BrandUsers", new[] { "User_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProduseFavorites");
            DropTable("dbo.CPUs");
            DropTable("dbo.UnitatiDeStocares");
            DropTable("dbo.PSUs");
            DropTable("dbo.PlacutaRAMs");
            DropTable("dbo.PastaCPUs");
            DropTable("dbo.Motherboards");
            DropTable("dbo.GPUs");
            DropTable("dbo.CosCumparaturis");
            DropTable("dbo.Cereres");
            DropTable("dbo.Carcasas");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BrandUsers");
        }
    }
}
