namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "sold", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "sold");
        }
    }
}
