namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ninethCatalog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produits", "FormuleGrX", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Produits", "FormuleGrY", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produits", "FormuleGrY", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Produits", "FormuleGrX", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
