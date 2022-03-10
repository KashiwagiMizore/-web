using HomeworkSubmit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DAL
{
    public class HomeworkVersionService : BaseService<Models.HomeworkVersion>, IDAL.IHomeworkVersionService
    {
        public HomeworkVersionService() : base(new HomeworkContext())
        {
        }
    }
}
