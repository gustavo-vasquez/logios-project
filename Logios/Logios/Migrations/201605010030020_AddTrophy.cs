namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrophy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trophies",
                c => new
                    {
                        TrophyId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrophyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trophies");
        }
    }
}
