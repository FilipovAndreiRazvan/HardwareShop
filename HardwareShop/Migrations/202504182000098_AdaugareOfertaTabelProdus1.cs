namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdaugareOfertaTabelProdus1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produse", "Oferta", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produse", "Oferta", c => c.Byte(nullable: false));
        }
    }
}
