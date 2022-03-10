using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.Models
{
    public class HomeworkContext:DbContext
    {
        public HomeworkContext():base("submit")
        {
            Database.SetInitializer<HomeworkContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TeacherComment> TeacherComments { get; set; }
        public DbSet<HomeworkVersion> HomeworkVersions { get; set; }
        public DbSet<HomeworkArticle> HomeworkArticles { get; set; }
        public DbSet<ClassNumber> ClassNumbers { get; set; }
        public DbSet<Notice> Notices { get; set; }
    }
}
