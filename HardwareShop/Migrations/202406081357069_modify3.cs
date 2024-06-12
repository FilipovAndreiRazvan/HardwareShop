namespace HardwareShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdresaLivrares", "Oras", c => c.String(nullable: false));
            AlterColumn("dbo.AdresaLivrares", "Localitate", c => c.String(nullable: false));
            AlterColumn("dbo.AdresaLivrares", "Strada", c => c.String(nullable: false));
            AlterColumn("dbo.AdresaLivrares", "numar", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdresaLivrares", "numar", c => c.String());
            AlterColumn("dbo.AdresaLivrares", "Strada", c => c.String());
            AlterColumn("dbo.AdresaLivrares", "Localitate", c => c.String());
            AlterColumn("dbo.AdresaLivrares", "Oras", c => c.String());
        }
    }
}
