namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GPUs", "CosCumparaturi_Id", "dbo.CosCumparaturis");
            DropIndex("dbo.GPUs", new[] { "CosCumparaturi_Id" });
            DropColumn("dbo.GPUs", "CosCumparaturi_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GPUs", "CosCumparaturi_Id", c => c.Int());
            CreateIndex("dbo.GPUs", "CosCumparaturi_Id");
            AddForeignKey("dbo.GPUs", "CosCumparaturi_Id", "dbo.CosCumparaturis", "Id");
        }
    }
}
