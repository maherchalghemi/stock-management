namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eigthCatalog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        ProduitId = c.Int(nullable: false, identity: true),
                        Marque = c.Int(nullable: false),
                        Libelle = c.String(nullable: false),
                        PrixHT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrixTTC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gratuite = c.Boolean(nullable: false),
                        FormuleGrX = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormuleGrY = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProduitId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Produits");
        }
    }
}
