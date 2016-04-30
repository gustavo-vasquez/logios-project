namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTopic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TopicId);
            
            AddColumn("dbo.Exercises", "Topic_TopicId", c => c.Int());
            CreateIndex("dbo.Exercises", "Topic_TopicId");
            AddForeignKey("dbo.Exercises", "Topic_TopicId", "dbo.Topics", "TopicId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "Topic_TopicId", "dbo.Topics");
            DropIndex("dbo.Exercises", new[] { "Topic_TopicId" });
            DropColumn("dbo.Exercises", "Topic_TopicId");
            DropTable("dbo.Topics");
        }
    }
}
