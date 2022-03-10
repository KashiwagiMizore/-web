namespace HomeworkSubmit.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HomeworkArticles", new[] { "Userid" });
            CreateIndex("dbo.HomeworkArticles", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.HomeworkArticles", new[] { "UserId" });
            CreateIndex("dbo.HomeworkArticles", "Userid");
        }
    }
}
