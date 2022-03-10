using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DTO
{
    public class HomeworkArticleDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string LoginName { get; set; }
        public Guid VersionId { get; set; }
        public string VersionName { get; set; }
        public int Score { get; set; }
        public bool IsTeacher { get; set; }//分成两个表单显示
        public bool IsStudent { get; set; }
        public bool IsAdmin { get; set; }
        public Guid ClassId { get; set; }
        public int ClassNum { get; set; }//用于自己班级显示
        public string Name { get; set; }
    }
}
