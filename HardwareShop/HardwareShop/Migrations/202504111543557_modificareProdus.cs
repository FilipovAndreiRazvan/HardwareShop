namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificareProdus : DbMigration
    {
        public override void Up()
        {
             DropColumn("dbo.Produse", "Brand");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produse", "Brand", c => c.String());
        }
    }
}
