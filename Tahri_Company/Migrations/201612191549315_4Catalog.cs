namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4Catalog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commercials",
                c => new
                    {
                        CommercialId = c.String(nullable: false, maxLength: 128),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                        Mdp = c.String(nullable: false, maxLength: 100),
                        ConfirmMdp = c.String(),
                    })
                .PrimaryKey(t => t.CommercialId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Commercials");
        }
    }
}
