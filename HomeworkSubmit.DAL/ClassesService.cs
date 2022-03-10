using HomeworkSubmit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DAL
{
    public class ClassesService:BaseService<Models.ClassNumber>,IDAL.IClassesService
    {
        private readonly Models.HomeworkContext _dbClass;
        public ClassesService(Models.HomeworkContext db) : base(new HomeworkContext())
        {
            _dbClass = db;
        }

        public IQueryable<ClassNumber> GetAllClassNumOrder()
        {
            return _dbClass.Set<Models.ClassNumber>()
                .Where(m => m.IsRemoved == false && m.ClassNum != 0).OrderBy(m=>m.ClassNum).AsNoTracking();//管理员班级为0 将其去除
        }

        public async Task<ClassNumber> GetOneClassByClassNumAsync(int classNum)
        {
            return await _dbClass.Set<Models.ClassNumber>().FirstOrDefaultAsync(m => m.ClassNum == classNum);
        }
    }
}
