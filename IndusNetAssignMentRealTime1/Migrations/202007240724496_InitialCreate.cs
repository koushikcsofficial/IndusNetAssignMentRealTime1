namespace IndusNetAssignMentRealTime1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserModels");
        }
    }
}
