using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DTO
{
    public class TeacherCommentDTO
    {
        public Guid Id { get; set; }
        public Guid Userid { get; set; }
        public string LoginName { get; set; }
        public Guid ArticleId { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
