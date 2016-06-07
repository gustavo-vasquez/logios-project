namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "ImagePath");
        }
    }
}
