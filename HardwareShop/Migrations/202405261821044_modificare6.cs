namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlacutaRAMs", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropIndex("dbo.PlacutaRAMs", new[] { "CosCumparaturi_Id" });
            DropColumn("dbo.PlacutaRAMs", "CosCumparaturi_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlacutaRAMs", "CosCumparaturi_Id", c => c.Int());
            CreateIndex("dbo.PlacutaRAMs", "CosCumparaturi_Id");
            AddForeignKey("dbo.PlacutaRAMs", "CosCumparaturi_Id", "dbo.CosCumparaturis", "Id");
        }
    }
}
