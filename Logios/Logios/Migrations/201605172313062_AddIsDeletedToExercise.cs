namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToExercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "IsDeleted");
        }
    }
}
