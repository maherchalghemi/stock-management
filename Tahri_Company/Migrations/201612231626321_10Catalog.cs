namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10Catalog : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Commercials");
            AddColumn("dbo.Commercials", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Commercials", "RegisterPassword", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Commercials", "ConfirmPassword", c => c.String());
            AddColumn("dbo.Commercials", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Commercials", "TelPerso", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Commercials", "Id");
            DropColumn("dbo.Commercials", "CommercialId");
            DropColumn("dbo.Commercials", "Telephone");
            DropColumn("dbo.Commercials", "DateNaissance");
            DropColumn("dbo.Commercials", "Mdp");
            DropColumn("dbo.Commercials", "ConfirmMdp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Commercials", "ConfirmMdp", c => c.String());
            AddColumn("dbo.Commercials", "Mdp", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Commercials", "DateNaissance", c => c.DateTime(nullable: false));
            AddColumn("dbo.Commercials", "Telephone", c => c.String(nullable: false));
            AddColumn("dbo.Commercials", "CommercialId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Commercials");
            DropColumn("dbo.Commercials", "TelPerso");
            DropColumn("dbo.Commercials", "BirthDate");
            DropColumn("dbo.Commercials", "ConfirmPassword");
            DropColumn("dbo.Commercials", "RegisterPassword");
            DropColumn("dbo.Commercials", "Id");
            AddPrimaryKey("dbo.Commercials", "CommercialId");
        }
    }
}
