namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PastaCPUs", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropIndex("dbo.PastaCPUs", new[] { "CosCumparaturi_Id" });
            DropColumn("dbo.PastaCPUs", "CosCumparaturi_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PastaCPUs", "CosCumparaturi_Id", c => c.Int());
            CreateIndex("dbo.PastaCPUs", "CosCumparaturi_Id");
            AddForeignKey("dbo.PastaCPUs", "CosCumparaturi_Id", "dbo.CosCumparaturis", "Id");
        }
    }
}
