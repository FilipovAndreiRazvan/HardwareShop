namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Motherboards", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropIndex("dbo.Motherboards", new[] { "CosCumparaturi_Id" });
            DropColumn("dbo.Motherboards", "CosCumparaturi_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Motherboards", "CosCumparaturi_Id", c => c.Int());
            CreateIndex("dbo.Motherboards", "CosCumparaturi_Id");
            AddForeignKey("dbo.Motherboards", "CosCumparaturi_Id", "dbo.CosCumparaturis", "Id");
        }
    }
}
