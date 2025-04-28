namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdaugareOfertaTabelProdus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produse", "Oferta", c => c.Byte(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produse", "Oferta");
        }
    }
}
