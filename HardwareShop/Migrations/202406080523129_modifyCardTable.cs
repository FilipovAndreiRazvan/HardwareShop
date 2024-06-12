namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyCardTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cards", "numeDetinator", c => c.String(nullable: false));
            AlterColumn("dbo.Cards", "nrCard", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cards", "nrCard", c => c.String());
            AlterColumn("dbo.Cards", "numeDetinator", c => c.String());
        }
    }
}
