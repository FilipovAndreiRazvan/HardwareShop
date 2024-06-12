namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PSUs", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropIndex("dbo.PSUs", new[] { "CosCumparaturi_Id" });
            DropColumn("dbo.PSUs", "CosCumparaturi_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PSUs", "CosCumparaturi_Id", c => c.Int());
            CreateIndex("dbo.PSUs", "CosCumparaturi_Id");
            AddForeignKey("dbo.PSUs", "CosCumparaturi_Id", "dbo.CosCumparaturis", "Id");
        }
    }
}
