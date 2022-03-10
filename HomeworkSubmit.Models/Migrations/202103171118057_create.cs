namespace HomeworkSubmit.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassNumbers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClassNum = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HomeworkArticles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Userid = c.Guid(nullable: false),
                        VersionId = c.Guid(nullable: false),
                        Score = c.Int(nullable: false),
                        ClassNumberId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassNumbers", t => t.ClassNumberId)
                .ForeignKey("dbo.HomeworkVersions", t => t.VersionId)
                .ForeignKey("dbo.Users", t => t.Userid)
                .Index(t => t.Userid)
                .Index(t => t.VersionId)
                .Index(t => t.ClassNumberId);
            
            CreateTable(
                "dbo.HomeworkVersions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VersionName = c.String(nullable: false, maxLength: 200),
                        UserId = c.Guid(nullable: false),
                        LimitDays = c.Int(nullable: false),
                        ClassNumberId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassNumbers", t => t.ClassNumberId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ClassNumberId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoginName = c.String(nullable: false, maxLength: 40),
                        Password = c.String(nullable: false, maxLength: 20),
                        Name = c.String(),
                        IsTeacher = c.Boolean(nullable: false),
                        IsStudent = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        ClassNumberId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassNumbers", t => t.ClassNumberId)
                .Index(t => t.ClassNumberId);
            
            CreateTable(
                "dbo.TeacherComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        UserId = c.Guid(nullable: false),
                        HomeworkArticleId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HomeworkArticles", t => t.HomeworkArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HomeworkArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.TeacherComments", "HomeworkArticleId", "dbo.HomeworkArticles");
            DropForeignKey("dbo.HomeworkArticles", "Userid", "dbo.Users");
            DropForeignKey("dbo.HomeworkArticles", "VersionId", "dbo.HomeworkVersions");
            DropForeignKey("dbo.HomeworkVersions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ClassNumberId", "dbo.ClassNumbers");
            DropForeignKey("dbo.HomeworkVersions", "ClassNumberId", "dbo.ClassNumbers");
            DropForeignKey("dbo.HomeworkArticles", "ClassNumberId", "dbo.ClassNumbers");
            DropIndex("dbo.TeacherComments", new[] { "HomeworkArticleId" });
            DropIndex("dbo.TeacherComments", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ClassNumberId" });
            DropIndex("dbo.HomeworkVersions", new[] { "ClassNumberId" });
            DropIndex("dbo.HomeworkVersions", new[] { "UserId" });
            DropIndex("dbo.HomeworkArticles", new[] { "ClassNumberId" });
            DropIndex("dbo.HomeworkArticles", new[] { "VersionId" });
            DropIndex("dbo.HomeworkArticles", new[] { "Userid" });
            DropTable("dbo.TeacherComments");
            DropTable("dbo.Users");
            DropTable("dbo.HomeworkVersions");
            DropTable("dbo.HomeworkArticles");
            DropTable("dbo.ClassNumbers");
        }
    }
}
