namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Depots",
                c => new
                    {
                        DepotId = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                        Tel = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DepotId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Depots");
        }
    }
}
