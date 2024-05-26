namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CosCumparaturis", "utilizator_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.CosCumparaturis", "utilizator_Id");
            AddForeignKey("dbo.CosCumparaturis", "utilizator_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CosCumparaturis", "utilizator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CosCumparaturis", new[] { "utilizator_Id" });
            DropColumn("dbo.CosCumparaturis", "utilizator_Id");
        }
    }
}
