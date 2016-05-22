namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToExercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Exercises", "User_Id");
            AddForeignKey("dbo.Exercises", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Exercises", new[] { "User_Id" });
            DropColumn("dbo.Exercises", "User_Id");
        }
    }
}
