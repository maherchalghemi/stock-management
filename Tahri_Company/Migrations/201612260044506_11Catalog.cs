namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11Catalog : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Commercials");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Commercials",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false),
                        RegisterPassword = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        TelPerso = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
