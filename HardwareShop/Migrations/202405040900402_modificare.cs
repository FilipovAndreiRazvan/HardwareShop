namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificare : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PastaCPUs", "conductivitateTermica", c => c.String(nullable: false));
            AlterColumn("dbo.PastaCPUs", "rezistentaTermica", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PastaCPUs", "rezistentaTermica", c => c.String());
            AlterColumn("dbo.PastaCPUs", "conductivitateTermica", c => c.String());
        }
    }
}
