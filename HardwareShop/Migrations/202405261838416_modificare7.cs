namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UnitatiDeStocares", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropIndex("dbo.UnitatiDeStocares", new[] { "CosCumparaturi_Id" });
            DropColumn("dbo.UnitatiDeStocares", "CosCumparaturi_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UnitatiDeStocares", "CosCumparaturi_Id", c => c.Int());
            CreateIndex("dbo.UnitatiDeStocares", "CosCumparaturi_Id");
            AddForeignKey("dbo.UnitatiDeStocares", "CosCumparaturi_Id", "dbo.CosCumparaturis", "Id");
        }
    }
}
