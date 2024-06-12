namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comandas", "card_Id", "dbo.Cards");
            DropIndex("dbo.Comandas", new[] { "card_Id" });
            DropColumn("dbo.Comandas", "card_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comandas", "card_Id", c => c.Int());
            CreateIndex("dbo.Comandas", "card_Id");
            AddForeignKey("dbo.Comandas", "card_Id", "dbo.Cards", "Id");
        }
    }
}
