namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UnitatiDeStocares", "dimensiune", c => c.String());
            DropColumn("dbo.UnitatiDeStocares", "dimensiuni");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UnitatiDeStocares", "dimensiuni", c => c.String());
            DropColumn("dbo.UnitatiDeStocares", "dimensiune");
        }
    }
}
