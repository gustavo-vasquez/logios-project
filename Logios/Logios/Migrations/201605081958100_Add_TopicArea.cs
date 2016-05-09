namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TopicArea : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopicAreaTopics",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        TopicAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.TopicAreaId })
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.TopicAreas", t => t.TopicAreaId, cascadeDelete: true)
                .Index(t => t.TopicId)
                .Index(t => t.TopicAreaId);
            
            CreateTable(
                "dbo.TopicAreas",
                c => new
                    {
                        TopicAreaId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TopicAreaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicAreaTopics", "TopicAreaId", "dbo.TopicAreas");
            DropForeignKey("dbo.TopicAreaTopics", "TopicId", "dbo.Topics");
            DropIndex("dbo.TopicAreaTopics", new[] { "TopicAreaId" });
            DropIndex("dbo.TopicAreaTopics", new[] { "TopicId" });
            DropTable("dbo.TopicAreas");
            DropTable("dbo.TopicAreaTopics");
        }
    }
}
