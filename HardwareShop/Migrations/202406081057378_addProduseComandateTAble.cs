namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProduseComandateTAble : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProduseComandates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdProdus = c.Int(nullable: false),
                        Categorie = c.String(),
                        Cantitate = c.Int(nullable: false),
                        IdComanda_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comandas", t => t.IdComanda_Id)
                .Index(t => t.IdComanda_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProduseComandates", "IdComanda_Id", "dbo.Comandas");
            DropIndex("dbo.ProduseComandates", new[] { "IdComanda_Id" });
            DropTable("dbo.ProduseComandates");
        }
    }
}
