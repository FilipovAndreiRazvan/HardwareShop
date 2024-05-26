namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addComandaTable1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comandas", "client_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comandas", new[] { "client_Id" });
            AddColumn("dbo.Comandas", "client", c => c.String());
            DropColumn("dbo.Comandas", "client_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comandas", "client_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Comandas", "client");
            CreateIndex("dbo.Comandas", "client_Id");
            AddForeignKey("dbo.Comandas", "client_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
