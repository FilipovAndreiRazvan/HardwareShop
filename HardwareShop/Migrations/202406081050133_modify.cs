namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comandas", "client_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comandas", "client_Id");
            AddForeignKey("dbo.Comandas", "client_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comandas", "client");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comandas", "client", c => c.String());
            DropForeignKey("dbo.Comandas", "client_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comandas", new[] { "client_Id" });
            DropColumn("dbo.Comandas", "client_Id");
        }
    }
}
