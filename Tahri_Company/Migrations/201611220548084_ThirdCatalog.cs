namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdCatalog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Impressions",
                c => new
                    {
                        ImpressionId = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Titre = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ImpressionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Impressions");
        }
    }
}
