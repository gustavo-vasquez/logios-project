namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsToExecise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "Development", c => c.String());
            AddColumn("dbo.Exercises", "Solution", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "Solution");
            DropColumn("dbo.Exercises", "Development");
        }
    }
}
