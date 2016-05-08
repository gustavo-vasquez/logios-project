namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_IdentityFromKeyFields : DbMigration
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
            AlterColumn("dbo.Exercises", "ExerciseId", c => c.Int(identity: false, nullable: false));
            AlterColumn("dbo.Topics", "TopicId", c => c.Int(identity: false, nullable: false));
            AlterColumn("dbo.Trophies", "TrophyId", c => c.Int(identity: false, nullable: false));
            AddPrimaryKey("dbo.Exercises", "ExerciseId");
            AddPrimaryKey("dbo.Topics", "TopicId");
            AddPrimaryKey("dbo.Trophies", "TrophyId");
            AddForeignKey("dbo.UserExercises", "ExerciseId", "dbo.Exercises", "ExerciseId", cascadeDelete: true);
            AddForeignKey("dbo.Exercises", "Topic_TopicId", "dbo.Topics", "TopicId");
            AddForeignKey("dbo.TopicAreaTopics", "TopicId", "dbo.Topics", "TopicId", cascadeDelete: true);
            AddForeignKey("dbo.UserTrophies", "TrophyId", "dbo.Trophies", "TrophyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTrophies", "TrophyId", "dbo.Trophies");
            DropForeignKey("dbo.TopicAreaTopics", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Exercises", "Topic_TopicId", "dbo.Topics");
            DropForeignKey("dbo.UserExercises", "ExerciseId", "dbo.Exercises");
            DropPrimaryKey("dbo.Trophies");
            DropPrimaryKey("dbo.Topics");
            DropPrimaryKey("dbo.Exercises");
            AlterColumn("dbo.Trophies", "TrophyId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Topics", "TopicId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Exercises", "ExerciseId", c => c.Int(nullable: false, identity: true));
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
