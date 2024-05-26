namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UnitatiDeStocares", "formFactor", c => c.String(nullable: false));
            AlterColumn("dbo.UnitatiDeStocares", "tipMemorie", c => c.String(nullable: false));
            AlterColumn("dbo.UnitatiDeStocares", "dimensiune", c => c.String(nullable: false));
            AlterColumn("dbo.UnitatiDeStocares", "lineUp", c => c.String(nullable: false));
            AlterColumn("dbo.UnitatiDeStocares", "tipController", c => c.String(nullable: false));
            AlterColumn("dbo.UnitatiDeStocares", "interfata", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UnitatiDeStocares", "interfata", c => c.String());
            AlterColumn("dbo.UnitatiDeStocares", "tipController", c => c.String());
            AlterColumn("dbo.UnitatiDeStocares", "lineUp", c => c.String());
            AlterColumn("dbo.UnitatiDeStocares", "dimensiune", c => c.String());
            AlterColumn("dbo.UnitatiDeStocares", "tipMemorie", c => c.String());
            AlterColumn("dbo.UnitatiDeStocares", "formFactor", c => c.String());
        }
    }
}
