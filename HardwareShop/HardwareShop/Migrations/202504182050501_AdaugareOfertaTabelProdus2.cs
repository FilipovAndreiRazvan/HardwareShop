namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdaugareOfertaTabelProdus2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produse", "Pret", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Produse", "Oferta", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produse", "Oferta", c => c.Byte());
            AlterColumn("dbo.Produse", "Pret", c => c.Single(nullable: false));
        }
    }
}
