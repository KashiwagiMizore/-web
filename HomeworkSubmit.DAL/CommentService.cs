using HomeworkSubmit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DAL
{
    public class CommentService : BaseService<Models.TeacherComment>, IDAL.ICommentService
    {
        //传入数据库上下文
        private readonly Models.HomeworkContext _dbComment;
        public CommentService(Models.HomeworkContext db) : base(new HomeworkContext())
        {
            _dbComment = db;
        }

        public IQueryable<TeacherComment> GetAllByArticleId(Guid articleId)
        {
            return _dbComment.Set<Models.TeacherComment>()
                .Where(m => m.IsRemoved == false && m.HomeworkArticleId == articleId).AsNoTracking();
        }
    }
}
