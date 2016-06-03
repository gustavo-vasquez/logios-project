namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReasonToTrophy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trophies", "Reason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trophies", "Reason");
        }
    }
}
