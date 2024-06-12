namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carcasas", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.GPUs", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.Motherboards", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.PastaCPUs", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.PlacutaRAMs", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.PSUs", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropForeignKey("dbo.UnitatiDeStocares", "ProdusFavorit_Id", "dbo.ProduseFavorites");
            DropIndex("dbo.Carcasas", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.PastaCPUs", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.Motherboards", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.GPUs", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.PlacutaRAMs", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.PSUs", new[] { "ProdusFavorit_Id" });
            DropIndex("dbo.UnitatiDeStocares", new[] { "ProdusFavorit_Id" });
            DropColumn("dbo.Carcasas", "ProdusFavorit_Id");
            DropColumn("dbo.PastaCPUs", "ProdusFavorit_Id");
            DropColumn("dbo.Motherboards", "ProdusFavorit_Id");
            DropColumn("dbo.GPUs", "ProdusFavorit_Id");
            DropColumn("dbo.PlacutaRAMs", "ProdusFavorit_Id");
            DropColumn("dbo.PSUs", "ProdusFavorit_Id");
            DropColumn("dbo.UnitatiDeStocares", "ProdusFavorit_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UnitatiDeStocares", "ProdusFavorit_Id", c => c.Int());
            AddColumn("dbo.PSUs", "ProdusFavorit_Id", c => c.Int());
            AddColumn("dbo.PlacutaRAMs", "ProdusFavorit_Id", c => c.Int());
            AddColumn("dbo.GPUs", "ProdusFavorit_Id", c => c.Int());
            AddColumn("dbo.Motherboards", "ProdusFavorit_Id", c => c.Int());
            AddColumn("dbo.PastaCPUs", "ProdusFavorit_Id", c => c.Int());
            AddColumn("dbo.Carcasas", "ProdusFavorit_Id", c => c.Int());
            CreateIndex("dbo.UnitatiDeStocares", "ProdusFavorit_Id");
            CreateIndex("dbo.PSUs", "ProdusFavorit_Id");
            CreateIndex("dbo.PlacutaRAMs", "ProdusFavorit_Id");
            CreateIndex("dbo.GPUs", "ProdusFavorit_Id");
            CreateIndex("dbo.Motherboards", "ProdusFavorit_Id");
            CreateIndex("dbo.PastaCPUs", "ProdusFavorit_Id");
            CreateIndex("dbo.Carcasas", "ProdusFavorit_Id");
            AddForeignKey("dbo.UnitatiDeStocares", "ProdusFavorit_Id", "dbo.ProduseFavorites", "Id");
            AddForeignKey("dbo.PSUs", "ProdusFavorit_Id", "dbo.ProduseFavorites", "Id");
            AddForeignKey("dbo.PlacutaRAMs", "ProdusFavorit_Id", "dbo.ProduseFavorites", "Id");
            AddForeignKey("dbo.PastaCPUs", "ProdusFavorit_Id", "dbo.ProduseFavorites", "Id");
            AddForeignKey("dbo.Motherboards", "ProdusFavorit_Id", "dbo.ProduseFavorites", "Id");
            AddForeignKey("dbo.GPUs", "ProdusFavorit_Id", "dbo.ProduseFavorites", "Id");
            AddForeignKey("dbo.Carcasas", "ProdusFavorit_Id", "dbo.ProduseFavorites", "Id");
        }
    }
}
