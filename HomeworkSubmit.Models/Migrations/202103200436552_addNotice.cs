namespace HomeworkSubmit.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNotice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        UserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notices", "UserId", "dbo.Users");
            DropIndex("dbo.Notices", new[] { "UserId" });
            DropTable("dbo.Notices");
        }
    }
}
