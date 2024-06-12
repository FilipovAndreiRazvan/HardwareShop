namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Facturas", "Adresa_Id", "dbo.AdresaLivrares");
            DropIndex("dbo.Facturas", new[] { "Adresa_Id" });
            AlterColumn("dbo.Facturas", "Nume", c => c.String(nullable: false));
            AlterColumn("dbo.Facturas", "Prenume", c => c.String(nullable: false));
            AlterColumn("dbo.Facturas", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Facturas", "Adresa_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Facturas", "Adresa_Id");
            AddForeignKey("dbo.Facturas", "Adresa_Id", "dbo.AdresaLivrares", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facturas", "Adresa_Id", "dbo.AdresaLivrares");
            DropIndex("dbo.Facturas", new[] { "Adresa_Id" });
            AlterColumn("dbo.Facturas", "Adresa_Id", c => c.Int());
            AlterColumn("dbo.Facturas", "Email", c => c.String());
            AlterColumn("dbo.Facturas", "Prenume", c => c.String());
            AlterColumn("dbo.Facturas", "Nume", c => c.String());
            CreateIndex("dbo.Facturas", "Adresa_Id");
            AddForeignKey("dbo.Facturas", "Adresa_Id", "dbo.AdresaLivrares", "Id");
        }
    }
}
