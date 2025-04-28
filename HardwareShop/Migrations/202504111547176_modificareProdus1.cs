namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificareProdus1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produse", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Produse", new[] { "BrandId" });
            AddForeignKey("dbo.Produse", "BrandId", "dbo.Branduri");   
        }
        
        public override void Down()
        {
        }
    }
}
