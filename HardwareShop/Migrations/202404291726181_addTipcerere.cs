namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTipcerere : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cereres", "tipCerere", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cereres", "tipCerere");
        }
    }
}
