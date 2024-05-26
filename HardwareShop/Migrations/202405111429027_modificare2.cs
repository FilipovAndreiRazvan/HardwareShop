namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CosCumparaturis", "utilizator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CosCumparaturis", new[] { "utilizator_Id" });
            AddColumn("dbo.CosCumparaturis", "utilizator", c => c.String());
            DropColumn("dbo.CosCumparaturis", "utilizator_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CosCumparaturis", "utilizator_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.CosCumparaturis", "utilizator");
            CreateIndex("dbo.CosCumparaturis", "utilizator_Id");
            AddForeignKey("dbo.CosCumparaturis", "utilizator_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
