namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comandas", "factura_Id", c => c.Int());
            CreateIndex("dbo.Comandas", "factura_Id");
            AddForeignKey("dbo.Comandas", "factura_Id", "dbo.Facturas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comandas", "factura_Id", "dbo.Facturas");
            DropIndex("dbo.Comandas", new[] { "factura_Id" });
            DropColumn("dbo.Comandas", "factura_Id");
        }
    }
}
