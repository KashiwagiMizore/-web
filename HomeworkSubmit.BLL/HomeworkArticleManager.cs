using HomeworkSubmit.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.BLL
{
    public class HomeworkArticleManager : IBLL.IHomeworkArticleManager
    {
        public async Task AddArticleAsync(string title, string content, Guid versionId, Guid userId,Guid classNumId)
        {
            using (var articleSvc = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                var article = new Models.HomeworkArticle()
                {
                    Title = title,
                    Content = content,
                    VersionId = versionId,
                    UserId = userId,
                    ClassNumberId=classNumId
                };
                await articleSvc.AddAsync(article);
            }
        }

        public async Task AddCommentAsync(Guid userId, Guid articleId, string content)
        {
            using (var commentService = new DAL.CommentService(new Models.HomeworkContext()))
            {
                await commentService.AddAsync(new Models.TeacherComment()
                {
                    Content = content,
                    UserId = userId,
                    HomeworkArticleId = articleId
                });
            }
        }



        public async Task AddVersionAsync(string versionName, Guid userId, int limitDays,Guid classNumId)
        {
            using (var versionSvc = new DAL.HomeworkVersionService())
            {
                await versionSvc.AddAsync(new Models.HomeworkVersion()
                {
                    VersionName = versionName,
                    UserId = userId,
                    LimitDays = limitDays,
                    ClassNumberId=classNumId
                });
            }
        }
        
        public async Task EditScoreAsync(Guid userId,Guid articleId ,int score)
        {
            using (var articleService=new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                var article = await articleService.GetOneByIdAsync(articleId);
                article.Score = score;
                await articleService.EditAsync(article);
            }
        }

        public async Task EditArticleAsync(Guid articleId, string title, string content)
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                var article = await articleService.GetOneByIdAsync(articleId);
                article.Content = content;
                article.Title = title;
                await articleService.EditAsync(article);
            }
        }

        public async Task EditVersionAsync(Guid versionId, string newVersionName, int limitDays)
        {
            using (var versionService = new DAL.HomeworkVersionService())
            {
                var version = await versionService.GetOneByIdAsync(versionId);
                version.VersionName = newVersionName;
                version.LimitDays = limitDays;
                await versionService.EditAsync(version);
            }
        }

        public async Task<bool> ExistAnyArticleInThisVersionAsync(Guid versionId)
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                return await articleService.GetAll().AnyAsync(m => m.VersionId == versionId);
            }
        }
        public async Task<bool> ExistArticleAsync(Guid articleId)
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                return await articleService.GetAll().AnyAsync(m => m.Id == articleId);
            }
        }

        public async Task<bool> ExistVersionAsync(Guid versionId)
        {
            using (var versionService = new DAL.HomeworkVersionService())
            {
                return await versionService.GetAll().AnyAsync(m => m.Id == versionId);
            }
        }

        public async Task<List<TeacherCommentDTO>> GetAllCommentsByArticleIdAsync(Guid articleId)
        {
            using (var commentService = new DAL.CommentService(new Models.HomeworkContext()))
            {
                return await commentService.GetAllByOrder(false).Where(m => m.HomeworkArticleId == articleId)
                    .Include(m => m.User)
                    .Select(m => new DTO.TeacherCommentDTO()
                    {
                        Id = m.Id,
                        Userid = m.UserId,
                        LoginName = m.User.LoginName,
                        ArticleId = m.HomeworkArticleId,
                        Content = m.Content,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
            }
        }

        public async Task<List<HomeworkArticleDTO>> GetAllHomeworkArticlesAsync()
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                return await articleService.GetAll().Include(m => m.User).Include(m => m.HomeworkVersion)
                    .Include(m=>m.ClassNumber)
                    .Select(m => new DTO.HomeworkArticleDTO()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        CreateTime = m.CreateTime,
                        LoginName = m.User.LoginName,
                        VersionId = m.VersionId,
                        VersionName = m.HomeworkVersion.VersionName,
                        IsTeacher = m.User.IsTeacher,
                        IsStudent=m.User.IsStudent,
                        IsAdmin=m.User.IsAdmin,
                        ClassId=m.ClassNumberId,
                        ClassNum=m.ClassNumber.ClassNum,
                        Score=m.Score,
                        Name=m.User.Name
                    }).ToListAsync();
            }
        }

        public async Task<List<HomeworkArticleDTO>> GetAllHomeworkArticlesByLoginNameAsync(string loginName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HomeworkArticleDTO>> GetAllHomeworkArticlesByUserIdAsync(Guid userId)
        {
            //using (var articleService = new DAL.HomeworkArticleService())
            //{
            //    var data= articleService.GetAll().Include(m => m.User).Include(m => m.HomeworkVersion)
            //        .Where(m => m.Userid==userId)
            //        .Select(m => new DTO.HomeworkArticleDTO()
            //        {
            //            ArticleId = m.Id,
            //            Title = m.Title,
            //            Content = m.Content,
            //            CreateTime = m.CreateTime,
            //            LoginName = m.User.LoginName,
            //            VersionId = m.HomeworkVersionId,
            //            VersionName = m.HomeworkVersion.VersionName
            //        }).ToList();
            //    return data;
            //}
            throw new NotImplementedException();
        }

        public async Task<List<HomeworkArticleDTO>> GetAllHomeworkArticlesByVersionIdAsync(Guid versionId)
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                return await articleService.GetAllByOrder(false).Include(m => m.User)
                    .Include(m => m.HomeworkVersion).Include(m => m.ClassNumber)
                    .Where(m => m.VersionId == versionId)
                    .Select(m => new DTO.HomeworkArticleDTO()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        CreateTime = m.CreateTime,
                        LoginName = m.User.LoginName,
                        VersionId = m.VersionId,
                        VersionName = m.HomeworkVersion.VersionName,
                        IsTeacher = m.User.IsTeacher,
                        IsStudent = m.User.IsStudent,
                        IsAdmin = m.User.IsAdmin,
                        ClassId=m.ClassNumberId,
                        ClassNum = m.ClassNumber.ClassNum,
                        Score =m.Score,
                        Name=m.User.Name
                    }).ToListAsync();
            }
        }

        public async Task<List<HomeworkVersionDTO>> GetAllVersionsAsync()
        {
            using (var versionService = new DAL.HomeworkVersionService())
            {
                return await versionService.GetAllByOrder(false).Include(m => m.User).Include(m=>m.ClassNumber)
                    .Select(m => new DTO.HomeworkVersionDTO()
                    {
                        Id = m.Id,
                        VersionName = m.VersionName,
                        LoginName = m.User.LoginName,
                        CreateTime = m.CreateTime,
                        LimitDays = m.LimitDays,
                        ClassId = m.ClassNumberId,
                        ClassNum =m.ClassNumber.ClassNum,
                        Name=m.User.Name
                    }).ToListAsync();
                //下段为获得一个类别中的所有文章Id 删除某个类别所有文章的评论用
                //using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
                //{
                //    foreach (var version in versionList)
                //    {
                //        var articles = await articleService.GetAll().Where(m => m.VersionId == version.Id).ToListAsync();
                //        version.ArticleIds = articles.Select(m => m.Id).ToArray();
                //    }
                //    return versionList;
                //}
                //在versionlist中无法传送guid[] 给删除方法 导致删除所有文章中的评论无法实现 暂时未解决
            }
        }

        public async Task<HomeworkArticleDTO> GetOneArticleByArticleIdAsync(Guid articleId)
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                return await articleService.GetAll().Include(m => m.User).Include(m => m.HomeworkVersion)
                    .Include(m => m.ClassNumber)
                    .Where(m => m.Id == articleId)
                    .Select(m => new DTO.HomeworkArticleDTO()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        CreateTime = m.CreateTime,
                        LoginName = m.User.LoginName,
                        VersionId = m.VersionId,
                        VersionName = m.HomeworkVersion.VersionName,
                        IsTeacher = m.User.IsTeacher,
                        IsStudent = m.User.IsStudent,
                        IsAdmin = m.User.IsAdmin,
                        ClassId = m.ClassNumberId,
                        ClassNum = m.ClassNumber.ClassNum,
                        Score =m.Score,
                        Name=m.User.Name
                    }).FirstAsync();
            }
        }

        public async Task<HomeworkVersionDTO> GetOneVersionByVersionIdAsync(Guid versionId)
        {
            using (var versionService = new DAL.HomeworkVersionService())
            {
                return await versionService.GetAll().Include(m => m.User).Include(m=>m.ClassNumber).Where(m => m.Id == versionId)
                    .Select(m => new DTO.HomeworkVersionDTO()
                    {
                        Id = m.Id,
                        VersionName = m.VersionName,
                        LoginName = m.User.LoginName,
                        CreateTime = m.CreateTime,
                        ClassId = m.ClassNumberId,
                        ClassNum = m.ClassNumber.ClassNum,
                        Name=m.User.Name
                    }).FirstAsync();
            }
        }



        public async Task TrueRemoveAllArticlesByVersionIdAsync(Guid versionId)
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                var articledata = articleService.GetAllByVersionId(versionId);
                foreach (var article in articledata)
                {
                    await articleService.TrueRemoveAsync(article, false);
                }
                await articleService.SaveAsync();
            }
        }

        public async Task TrueRemoveAllCommentsByArticleIdAsync(Guid articleId)
        {
            using (var commentService = new DAL.CommentService(new Models.HomeworkContext()))
            {
                var commentData = commentService.GetAllByArticleId(articleId);
                foreach (var comment in commentData)
                {
                    await commentService.TrueRemoveAsync(comment, false);//暂时先不保存
                }
                await commentService.SaveAsync();//遍历完成进行保存
            }
        }


        public async Task TrueRemoveOneArticleAsync(Guid articleId)
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                var article = await articleService.GetOneByIdAsync(articleId);
                await articleService.TrueRemoveAsync(article);
            }
        }
        public async Task TrueRemoveOneVersionAsync(Guid versionId)
        {
            using (var versionService = new DAL.HomeworkVersionService())
            {
                var version = await versionService.GetOneByIdAsync(versionId);
                await versionService.TrueRemoveAsync(version);
            }
        }

        public async Task<bool> JudgeAuthorisTeacherAsync(Guid articleId)
        {
            using (var articleService=new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                return await articleService.GetAll().Include(m=>m.User).
                    AnyAsync(m => m.Id == articleId && m.User.IsTeacher == true);
            }
        }

        public async Task<bool> JudgeAuthorisMyselfAsync(Guid articleId,Guid userId)
        {
            using (var articleService = new DAL.HomeworkArticleService(new Models.HomeworkContext()))
            {
                return await articleService.GetAll().Include(m => m.User)
                    .AnyAsync(m => m.Id == articleId && m.User.Id == userId);
            }
        }
    }
}
