namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificareCerere : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cereri", "Carcase");
            DropColumn("dbo.Cereri", "PlaciDeBaza");
            DropColumn("dbo.Cereri", "Procesoare");
            DropColumn("dbo.Cereri", "PlaciVideo");
            DropColumn("dbo.Cereri", "PlacuteRAM");
            DropColumn("dbo.Cereri", "UnitatiDeStocare");
            DropColumn("dbo.Cereri", "Surse");
            DropColumn("dbo.Cereri", "PastaProcesor");
            DropColumn("dbo.Cereri", "TipCerere");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cereri", "TipCerere", c => c.String());
            AddColumn("dbo.Cereri", "PastaProcesor", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cereri", "Surse", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cereri", "UnitatiDeStocare", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cereri", "PlacuteRAM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cereri", "PlaciVideo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cereri", "Procesoare", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cereri", "PlaciDeBaza", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cereri", "Carcase", c => c.Boolean(nullable: false));
        }
    }
}
