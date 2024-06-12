namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFacturaTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nume = c.String(),
                        Prenume = c.String(),
                        Email = c.String(),
                        NrTelefon = c.Int(nullable: false),
                        Adresa_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdresaLivrares", t => t.Adresa_Id)
                .Index(t => t.Adresa_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facturas", "Adresa_Id", "dbo.AdresaLivrares");
            DropIndex("dbo.Facturas", new[] { "Adresa_Id" });
            DropTable("dbo.Facturas");
        }
    }
}
