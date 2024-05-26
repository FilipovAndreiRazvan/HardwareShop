namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PSUs", "alimentare", c => c.String(nullable: false));
            AlterColumn("dbo.PSUs", "format", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PSUs", "format", c => c.String());
            AlterColumn("dbo.PSUs", "alimentare", c => c.String());
        }
    }
}
