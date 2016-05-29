namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToTopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "IsDeleted");
        }
    }
}
