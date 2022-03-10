using HomeworkSubmit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DAL
{
    public class NoticeService : BaseService<Models.Notice>, IDAL.INoticeService
    {
        public NoticeService() : base(new HomeworkContext())
        {
        }
    }
}
