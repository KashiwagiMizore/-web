using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DTO
{
    public class UserInformationDTO
    {
        public Guid Id { get; set; }//这里的Id加前缀会导致自动生成的页面出现问题 或者把之后的item.Id修改掉
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }//昵称
        public Guid ClassId { get; set; }
        public int ClassNum { get; set; }
        public bool IsStudent { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsTeacher { get; set; }
    }
}
