namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carcasas", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropIndex("dbo.Carcasas", new[] { "CosCumparaturi_Id" });
            DropColumn("dbo.Carcasas", "CosCumparaturi_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carcasas", "CosCumparaturi_Id", c => c.Int());
            CreateIndex("dbo.Carcasas", "CosCumparaturi_Id");
            AddForeignKey("dbo.Carcasas", "CosCumparaturi_Id", "dbo.CosCumparaturis", "Id");
        }
    }
}
