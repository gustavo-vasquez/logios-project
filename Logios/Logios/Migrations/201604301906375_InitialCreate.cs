namespace Logios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseId = c.Int(nullable: false, identity: true),
                        Problem = c.String(),
                    })
                .PrimaryKey(t => t.ExerciseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Exercises");
        }
    }
}
