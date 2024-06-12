namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comandas", "pretComanda", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comandas", "pretComanda");
        }
    }
}
