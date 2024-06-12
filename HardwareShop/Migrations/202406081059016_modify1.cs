namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comandas", "CategorieProduse");
            DropColumn("dbo.Comandas", "IndexProduse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comandas", "IndexProduse", c => c.String());
            AddColumn("dbo.Comandas", "CategorieProduse", c => c.String());
        }
    }
}
