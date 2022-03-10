using HomeworkSubmit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DAL
{
    public class HomeworkArticleService : BaseService<Models.HomeworkArticle>, IDAL.IHomeworkArticleService
    {
        private readonly Models.HomeworkContext _dbArticle;
        public HomeworkArticleService(Models.HomeworkContext db) : base(new HomeworkContext())
        {
            _dbArticle = db;
        }

        public IQueryable<HomeworkArticle> GetAllByVersionId(Guid versionId)
        {
            return _dbArticle.Set<HomeworkArticle>()
                .Where(m => m.IsRemoved == false && m.VersionId == versionId).AsNoTracking();
        }
    }
}
