namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNrBucToCosCumparaturi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CosCumparaturis", "nrBuc", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CosCumparaturis", "nrBuc");
        }
    }
}
