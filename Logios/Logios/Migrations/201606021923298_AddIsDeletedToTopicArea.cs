namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToTopicArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TopicAreas", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TopicAreas", "IsDeleted");
        }
    }
}
