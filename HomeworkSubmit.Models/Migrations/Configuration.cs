namespace HomeworkSubmit.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeworkSubmit.Models.HomeworkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HomeworkSubmit.Models.HomeworkContext context)
        {
            //context.Users.AddOrUpdate
            //(
            //    new User()
            //    {
            //        LoginName = "teacher1",
            //        Password = "teacher1",
            //        Name = "教师1",
            //        IsTeacher = true,
            //        IsStudent = false,
            //        ClassNumber = new ClassNumber() { ClassNum = 1, Id = Guid.NewGuid() }
            //    }
            //);
            //context.Users.AddOrUpdate
            //(
            //    new User()
            //    {
            //        LoginName = "teacher2",
            //        Password = "teacher2",
            //        Name = "教师2",
            //        IsTeacher = true,
            //        IsStudent = false,
            //        ClassNumber = new ClassNumber() { ClassNum = 2, Id = Guid.NewGuid() }
            //    }
            //);
            //context.Users.AddOrUpdate
            //(
            //    new User()
            //    {
            //        LoginName = "admin",
            //        Password = "admin",
            //        Name = "管理员",
            //        IsAdmin = true,
            //        IsStudent = false,
            //        ClassNumber = new ClassNumber() { ClassNum = 0, Id = Guid.NewGuid() }
            //    }
            //);
        }
    }
}
