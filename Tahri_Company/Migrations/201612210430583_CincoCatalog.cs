namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CincoCatalog : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Commercials");
            AlterColumn("dbo.Commercials", "CommercialId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Commercials", "CommercialId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Commercials");
            AlterColumn("dbo.Commercials", "CommercialId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Commercials", "CommercialId");
        }
    }
}
