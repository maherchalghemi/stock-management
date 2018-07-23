namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maher3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockDepots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepotId = c.Int(nullable: true),
                        qte = c.Int(nullable: true),
                        designation = c.String(nullable: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockDepots");
        }
    }
}
