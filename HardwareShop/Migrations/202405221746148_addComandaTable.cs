namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addComandaTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comandas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategorieProduse = c.String(),
                        IndexProduse = c.String(),
                        adresa_Id = c.Int(),
                        card_Id = c.Int(),
                        client_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdresaLivrares", t => t.adresa_Id)
                .ForeignKey("dbo.Cards", t => t.card_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.client_Id)
                .Index(t => t.adresa_Id)
                .Index(t => t.card_Id)
                .Index(t => t.client_Id);
            
            CreateTable(
                "dbo.AdresaLivrares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Oras = c.String(),
                        Localitate = c.String(),
                        Strada = c.String(),
                        bloc = c.String(),
                        numar = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        numeDetinator = c.String(),
                        nrCard = c.String(),
                        CVC = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comandas", "client_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comandas", "card_Id", "dbo.Cards");
            DropForeignKey("dbo.Comandas", "adresa_Id", "dbo.AdresaLivrares");
            DropIndex("dbo.Comandas", new[] { "client_Id" });
            DropIndex("dbo.Comandas", new[] { "card_Id" });
            DropIndex("dbo.Comandas", new[] { "adresa_Id" });
            DropTable("dbo.Cards");
            DropTable("dbo.AdresaLivrares");
            DropTable("dbo.Comandas");
        }
    }
}
