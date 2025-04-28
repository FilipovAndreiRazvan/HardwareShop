namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificareNumeProdus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produse", "Nume", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produse", "Nume", c => c.String(nullable: false));
        }
    }
}
