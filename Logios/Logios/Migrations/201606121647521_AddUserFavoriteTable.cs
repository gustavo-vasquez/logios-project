namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFavoriteTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFavorites",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ExerciseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ExerciseId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ExerciseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFavorites", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.UserFavorites", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserFavorites", new[] { "ExerciseId" });
            DropIndex("dbo.UserFavorites", new[] { "UserId" });
            DropTable("dbo.UserFavorites");
        }
    }
}
