namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificareCosCumparaturi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CosCumparaturi", "CategorieId", "dbo.Categorii");
            DropIndex("dbo.CosCumparaturi", new[] { "CategorieId" });
            DropColumn("dbo.CosCumparaturi", "CategorieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CosCumparaturi", "CategorieId", c => c.Byte(nullable: false));
            CreateIndex("dbo.CosCumparaturi", "CategorieId");
            AddForeignKey("dbo.CosCumparaturi", "CategorieId", "dbo.Categorii", "Id", cascadeDelete: true);
        }
    }
}
