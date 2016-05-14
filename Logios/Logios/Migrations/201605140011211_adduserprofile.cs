namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserprofile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserExercises", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "Topic_TopicId", "dbo.Topics");
            DropForeignKey("dbo.TopicAreaTopics", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.UserTrophies", "TrophyId", "dbo.Trophies");
            DropPrimaryKey("dbo.Exercises");
            DropPrimaryKey("dbo.Topics");
            DropPrimaryKey("dbo.Trophies");
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            AlterColumn("dbo.Exercises", "ExerciseId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Topics", "TopicId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Trophies", "TrophyId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Exercises", "ExerciseId");
            AddPrimaryKey("dbo.Topics", "TopicId");
            AddPrimaryKey("dbo.Trophies", "TrophyId");
            AddForeignKey("dbo.UserExercises", "ExerciseId", "dbo.Exercises", "ExerciseId", cascadeDelete: true);
            AddForeignKey("dbo.Exercises", "Topic_TopicId", "dbo.Topics", "TopicId");
            AddForeignKey("dbo.TopicAreaTopics", "TopicId", "dbo.Topics", "TopicId", cascadeDelete: true);
            AddForeignKey("dbo.UserTrophies", "TrophyId", "dbo.Trophies", "TrophyId", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "Points");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Points", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserTrophies", "TrophyId", "dbo.Trophies");
            DropForeignKey("dbo.TopicAreaTopics", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Exercises", "Topic_TopicId", "dbo.Topics");
            DropForeignKey("dbo.UserExercises", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.UserProfiles", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.UserProfiles", new[] { "UserID" });
            DropPrimaryKey("dbo.Trophies");
            DropPrimaryKey("dbo.Topics");
            DropPrimaryKey("dbo.Exercises");
            AlterColumn("dbo.Trophies", "TrophyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Topics", "TopicId", c => c.Int(nullable: false));
            AlterColumn("dbo.Exercises", "ExerciseId", c => c.Int(nullable: false));
            DropTable("dbo.UserProfiles");
            AddPrimaryKey("dbo.Trophies", "TrophyId");
            AddPrimaryKey("dbo.Topics", "TopicId");
            AddPrimaryKey("dbo.Exercises", "ExerciseId");
            AddForeignKey("dbo.UserTrophies", "TrophyId", "dbo.Trophies", "TrophyId", cascadeDelete: true);
            AddForeignKey("dbo.TopicAreaTopics", "TopicId", "dbo.Topics", "TopicId", cascadeDelete: true);
            AddForeignKey("dbo.Exercises", "Topic_TopicId", "dbo.Topics", "TopicId");
            AddForeignKey("dbo.UserExercises", "ExerciseId", "dbo.Exercises", "ExerciseId", cascadeDelete: true);
        }
    }
}
