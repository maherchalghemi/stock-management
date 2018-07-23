namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nineCatalog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produits", "Gratuite", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produits", "Gratuite", c => c.Boolean(nullable: false));
        }
    }
}
