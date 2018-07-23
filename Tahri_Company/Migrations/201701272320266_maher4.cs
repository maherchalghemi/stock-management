namespace Tahri_Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maher4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commandes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommertialId = c.Int(nullable: false),
                        NomCli = c.String(nullable: false),
                        AdresseCli = c.String(nullable: false),
                        TelCli = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemCmds",
                c => new
                    {
                        ItemCmdId = c.Int(nullable: false, identity: true),
                        CmdId = c.Int(nullable: false),
                        qte = c.Int(nullable: false),
                        depot = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ItemCmdId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ItemCmds");
            DropTable("dbo.Commandes");
        }
    }
}
