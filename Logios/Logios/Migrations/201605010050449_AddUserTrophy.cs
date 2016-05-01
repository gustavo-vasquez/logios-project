namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTrophy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTrophies",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TrophyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TrophyId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Trophies", t => t.TrophyId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TrophyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTrophies", "TrophyId", "dbo.Trophies");
            DropForeignKey("dbo.UserTrophies", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserTrophies", new[] { "TrophyId" });
            DropIndex("dbo.UserTrophies", new[] { "UserId" });
            DropTable("dbo.UserTrophies");
        }
    }
}
