using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DTO
{
    public class HomeworkVersionDTO
    {
        public Guid Id { get; set; }
        public string VersionName { get; set; }
        public string LoginName { get; set; }
        public DateTime CreateTime { get; set; }
        public int LimitDays { get; set; }//获取某次作业的过期时间
        public Guid ClassId { get; set; }
        public int ClassNum { get; set; }//只显示自己班级的
        public string Name { get; set; }
        //public Guid[] ArticleIds { get; set; }
    }
}
