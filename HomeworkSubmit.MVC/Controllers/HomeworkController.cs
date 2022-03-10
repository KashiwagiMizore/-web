using HomeworkSubmit.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeworkSubmit.MVC.Controllers
{
    [LoginAuth]
    public class HomeworkController : Controller
    {
        // GET: Homework
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [TeacherOrAdminAuth]
        public ActionResult AddHomeworkVersion()
        {
            return View();
        }
        [HttpPost]
        [TeacherOrAdminAuth]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddHomeworkVersion(Models.HomeworkViewModels.AddVersionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var articleManager = new BLL.HomeworkArticleManager();
                var userId = Guid.Parse(Session["userId"].ToString());
                var classNumId = Guid.Parse(Session["classNumId"].ToString());
                await articleManager.AddVersionAsync(model.VersionName, userId, model.LimitDays, classNumId);
                //return RedirectToAction("VersionList");
                return Content("<script>alert('添加成功');window.location.href='/Homework/VersionList';</script>");
            }
            else
            {
                ModelState.AddModelError("", "输入错误");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> VersionList()
        {
            ViewBag.IsTeacher = bool.Parse(Session["isTeacher"].ToString());
            ViewBag.IsStudent = bool.Parse(Session["IsStudent"].ToString());
            ViewBag.IsAdmin = bool.Parse(Session["IsAdmin"].ToString());
            ViewBag.classNum = int.Parse(Session["classNum"].ToString());//获取当前登录用户班级
            return View(await new BLL.HomeworkArticleManager().GetAllVersionsAsync());
        }

        [HttpGet]
        public async Task<ActionResult> AddArticle(Guid? id, DateTime deadLine)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(ArticleList));
            }
            if (await new BLL.HomeworkArticleManager().ExistVersionAsync(id.Value) == false)
            {
                return RedirectToAction(nameof(ArticleList));
            }
            var isTeacher = bool.Parse(Session["isTeacher"].ToString());//获取当前用户的权限
            var isStudent = bool.Parse(Session["isStudent"].ToString());
            var isAdmin = bool.Parse(Session["isAdmin"].ToString());
            var timeSpan = DateTime.Now - deadLine;//时间为自定义
            var timeDataMinutes = timeSpan.TotalMinutes;
            var timeDataHours = timeSpan.TotalHours;
            var timeDataDays = timeSpan.TotalDays;
            if (timeDataDays >= 0 && timeDataHours >= 0 && timeDataMinutes > 0 && isTeacher == false && isAdmin == false)
            {
                return Content("<script>alert('已超时 无法提交');window.location.href='/Homework/ArticleList';</script>");
            }
            else
            {
                return View();
            }
            //ViewBag.VersionIds = await new BLL.HomeworkArticleManager().GetAllVersionsAsync(); //用于测试            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> AddArticle(Models.HomeworkViewModels.AddArticleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var classNumId = Guid.Parse(Session["classNumId"].ToString());//获取当前用户班级
                var userId = Guid.Parse(Session["userId"].ToString());
                //model中的Id和Get中guid? id相等 为versionId 可通过调试得到
                await new BLL.HomeworkArticleManager().AddArticleAsync(model.Title, model.Content, model.Id, userId, classNumId);
                //return Content("添加成功");
                //return RedirectToAction("ArticleList");
                return Content("<script>alert('添加成功');window.location.href='/Homework/ArticleList';</script>");
            }
            ModelState.AddModelError("", "输入错误");
            return View(model);
        }

        [HttpGet]
        //只有此处的datetime无法为空 需要设置问号 原因暂时不明
        public async Task<ActionResult> ArticleList(Guid? id, DateTime? deadLine)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(VersionList));
            }
            if (await new BLL.HomeworkArticleManager().ExistVersionAsync(id.Value) == false)
            {
                return RedirectToAction(nameof(VersionList));
            }
            ViewBag.VersionId = id;//将versionid传到前端
            ViewBag.NowLogin = Session["loginName"].ToString();//获取当前登录用户
            ViewBag.classNum = int.Parse(Session["classNum"].ToString());//获取当前登录用户班级
            ViewBag.IsTeacher = bool.Parse(Session["isTeacher"].ToString());//获取当前用户的权限
            ViewBag.IsStudent = bool.Parse(Session["IsStudent"].ToString());
            ViewBag.IsAdmin = bool.Parse(Session["IsAdmin"].ToString());
            ViewBag.DeadLine = deadLine;//该次作业的截止时间
            //只显示该次作业中的作业 不显示全部
            var articles = await new BLL.HomeworkArticleManager().GetAllHomeworkArticlesByVersionIdAsync(id.Value);
            return View(articles);
        }

        [HttpGet]
        //学生用户在作业截止之前不能看其他学生的作业 任何人都可以无条件看老师的 老师可以无条件看任何人的
        //在之后做出修改
        public async Task<ActionResult> ArticleDetails(Guid? id, string loginName, DateTime deadLine)
        {
            var articleManager = new BLL.HomeworkArticleManager();
            if (id == null)
            {
                return RedirectToAction(nameof(ArticleList));
            }
            if (await new BLL.HomeworkArticleManager().ExistArticleAsync(id.Value) == false)
            {
                return RedirectToAction(nameof(ArticleList));
            }
            ViewBag.Comments = await articleManager.GetAllCommentsByArticleIdAsync(id.Value);
            //注意用户权限不能通过actionlink传过来 只能在session中获取 否则直接通过url访问会可以进行管理员操作
            var isTeacher = bool.Parse(Session["isTeacher"].ToString());//获取当前用户的权限 
            var isStudent = bool.Parse(Session["isStudent"].ToString());//获取当前用户的权限 
            var isAdmin = bool.Parse(Session["isAdmin"].ToString());//获取当前用户的权限 
            var author = loginName;//该篇文章的作者
            var nowLogin = Session["loginName"].ToString();//获取当前登录用户
            var userId = Guid.Parse(Session["userId"].ToString());
            ViewBag.IsTechaer = isTeacher;
            ViewBag.isStudent = isStudent;
            ViewBag.isAdmin = isAdmin;
            await articleManager.GetOneArticleByArticleIdAsync(id.Value);
            if (isTeacher || isAdmin)
            {
                return View(await articleManager.GetOneArticleByArticleIdAsync(id.Value));
            }
            else
            {
                var timeSpan = DateTime.Now - deadLine;//时间自定义
                var timeDataMinutes = timeSpan.TotalMinutes;
                var timeDataHours = timeSpan.TotalHours;
                var timeDataDays = timeSpan.TotalDays;
                if (await articleManager.JudgeAuthorisTeacherAsync(id.Value)
                    || await articleManager.JudgeAuthorisMyselfAsync(id.Value, userId))
                {
                    return View(await articleManager.GetOneArticleByArticleIdAsync(id.Value));
                }
                else if (timeDataDays <= 0 && timeDataHours <= 0 && timeDataMinutes < 0 && author != nowLogin)
                {
                    return Content("<script>alert('截止时间前无法查看');" +
                        "window.location.href='/Homework/ArticleList';</script>");
                }
                else
                {
                    return View(await articleManager.GetOneArticleByArticleIdAsync(id.Value));
                }
            }
        }

        [HttpGet]
        //要检测文章发布者和当前登录用户 只有一致才能进行修改
        public async Task<ActionResult> EditArticle(Guid? id, string loginName, DateTime deadLine)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(ArticleList));
            }
            if (await new BLL.HomeworkArticleManager().ExistArticleAsync(id.Value) == false)
            {
                return RedirectToAction(nameof(ArticleList));
            }
            //超时后无法编辑
            var timeSpan = DateTime.Now - deadLine;//时间自定义
            var timeDataMinutes = timeSpan.TotalMinutes;
            var timeDataHours = timeSpan.TotalHours;
            var timeDataDays = timeSpan.TotalDays;
            var isTeacher = bool.Parse(Session["isTeacher"].ToString());//获取当前用户的权限
            var isStudent = bool.Parse(Session["isStudent"].ToString());
            var isAdmin = bool.Parse(Session["isAdmin"].ToString());
            if (timeDataDays >= 0 && timeDataHours >= 0 && timeDataMinutes > 0 && isTeacher == false && isAdmin == false)
            {
                return Content("<script>alert('已超时 无法编辑');window.location.href='/Homework/ArticleList';</script>");
            }
            //过滤操作要保留 防止用户通过url直接进入该方法跨权限修改
            var author = loginName;//该篇文章的作者
            var nowLogin = Session["loginName"].ToString();//获取当前登录用户
            if (author == nowLogin)
            {
                var articleManager = new BLL.HomeworkArticleManager();
                var data = await articleManager.GetOneArticleByArticleIdAsync(id.Value);
                //从dto转换为viewmodel 就一个对象 所以只需要实例化 不需要Select和Tolist方法
                return View(new Models.HomeworkViewModels.EditArticleViewModel()
                {
                    Title = data.Title,
                    Content = data.Content,
                    Id = data.Id//让DTO中的ID传到ViewModel中
                });
            }
            else if (isAdmin == true)
            {
                var articleManager = new BLL.HomeworkArticleManager();
                var data = await articleManager.GetOneArticleByArticleIdAsync(id.Value);
                //从dto转换为viewmodel 就一个对象 所以只需要实例化 不需要Select和Tolist方法
                return View(new Models.HomeworkViewModels.EditArticleViewModel()
                {
                    Title = data.Title,
                    Content = data.Content,
                    Id = data.Id//让DTO中的ID传到ViewModel中
                });
            }
            else
            {
                //return Content("无法修改别人发布的内容");
                return Content("<script>alert('无法修改别人发布的内容');" +
                    "window.location.href='/Homework/ArticleList';</script>");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> EditArticle(Models.HomeworkViewModels.EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var author = model.LoginName;//该篇文章的作者
                //var loginName = Session["loginName"].ToString();//获取当前登录用户
                var articleManager = new BLL.HomeworkArticleManager();
                await articleManager.EditArticleAsync(model.Id, model.Title, model.Content);
                //return RedirectToAction(nameof(ArticleList));
                return Content("<script>alert('修改成功');window.location.href='/Homework/ArticleList';</script>");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [TeacherOrAdminAuth] //过滤操作要保留 防止用户通过url直接进入该方法跨权限修改
        public async Task<ActionResult> EditVersion(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(VersionList));
            }
            if (await new BLL.HomeworkArticleManager().ExistVersionAsync(id.Value) == false)
            {
                return RedirectToAction(nameof(VersionList));
            }
            var versionManager = new BLL.HomeworkArticleManager();
            var data = await versionManager.GetOneVersionByVersionIdAsync(id.Value);
            return View(new Models.HomeworkViewModels.EditVersionViewModel()
            {
                Id = data.Id,
                VersionName = data.VersionName
            });
        }
        [HttpPost]
        [TeacherOrAdminAuth]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVersion(Models.HomeworkViewModels.EditVersionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var versionManager = new BLL.HomeworkArticleManager();
                await versionManager.EditVersionAsync(model.Id, model.VersionName, model.LimitDays);
                //return RedirectToAction(nameof(VersionList));
                return Content("<script>alert('修改成功');window.location.href='/Homework/VersionList';</script>");
            }
            return View(model);
        }

        [HttpPost]
        [TeacherOrAdminAuth]
        //采用ajax方法
        public async Task<ActionResult> AddComment(Models.HomeworkViewModels.AddCommentViewModel model)
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            var articleManager = new BLL.HomeworkArticleManager();
            await articleManager.AddCommentAsync(userId, model.Id, model.Content);//id为文章id
            return Json(new { result = "ok" });
        }

        [HttpPost]
        [TeacherOrAdminAuth]
        //采用ajax方法
        public async Task<ActionResult> AddScore(Models.HomeworkViewModels.AddScoreViewModel model)
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            var articleManager = new BLL.HomeworkArticleManager();
            await articleManager.EditScoreAsync(userId, model.Id, model.Score);//id为文章id
            return Json(new { result = "ok" });
        }
        [HttpPost]
        [TeacherOrAdminAuth]
        //采用ajax方法
        public async Task<ActionResult> DeleteScore(Models.HomeworkViewModels.DeleteScoreViewModel model)
        {
            var articleManager = new BLL.HomeworkArticleManager();
            var userId = Guid.Parse(Session["userId"].ToString());
            await articleManager.EditScoreAsync(userId, model.Id, 0);//id为文章id
            return Json(new { result = "ok" });
        }
        [HttpPost]
        [TeacherOrAdminAuth]
        //采用ajax方法
        public async Task<ActionResult> DeleteComments(Models.HomeworkViewModels.DeleteCommentViewModel model)
        {
            var articleManager = new BLL.HomeworkArticleManager();
            await articleManager.TrueRemoveAllCommentsByArticleIdAsync(model.Id);
            return Json(new { result = "ok" });
        }
        [HttpGet]
        [TeacherOrAdminAuth]
        public async Task<ActionResult> DeleteArticle(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(ArticleList));
            }
            if (await new BLL.HomeworkArticleManager().ExistArticleAsync(id.Value) == false)
            {
                return RedirectToAction(nameof(ArticleList));
            }
            return View();
        }
        [HttpPost]//此处只能post提交删除 不能用delete 原因暂时未知
        [ValidateAntiForgeryToken]//上面的添加评论增加防伪戳之后出现bug 删除方法未知是否会出现bug
        [TeacherOrAdminAuth] //过滤操作要保留 防止用户通过url直接进入该方法跨权限删除
        //要删除文章 得先删除其中的所有评论
        public async Task<ActionResult> DeleteArticle(Models.HomeworkViewModels.DeleteArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var articleManager = new BLL.HomeworkArticleManager();
                await articleManager.TrueRemoveAllCommentsByArticleIdAsync(model.Id);
                await articleManager.TrueRemoveOneArticleAsync(model.Id);
                return Content("<script>alert('删除成功');window.location.href='/Homework/ArticleList';</script>");
            }
            return View(model);
        }

        [HttpGet]
        [TeacherOrAdminAuth]
        public async Task<ActionResult> DeleteVersion(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(VersionList));
            }
            if (await new BLL.HomeworkArticleManager().ExistVersionAsync(id.Value) == false)
            {
                return RedirectToAction(nameof(VersionList));
            }
            if (await new BLL.HomeworkArticleManager().ExistAnyArticleInThisVersionAsync(id.Value) == true)
            {
                return Content("<script>alert('该目录下有文章 无法进行删除');" +
                    "window.location.href='/Homework/VersionList';</script>");
            }
            //foreach (var article in articleIds)
            //{
            //    var testdata = article;
            //}
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [TeacherOrAdminAuth] //过滤操作要保留 防止用户通过url直接进入该方法跨权限删除
        //注意要删除第几次作业也同理 必须先删除其中所有的文章和评论
        public async Task<ActionResult> DeleteVersion(Models.HomeworkViewModels.DeleteVersionViewModel model)
        {
            //由于versionlist中无法向delete页面传输guid[]数组 测试得到结果为null 暂时先选择手动删除所有的评论 文章之后再进行删除
            //或者在文章中没有评论的时候可以生效
            //可以试着改为ajax方法或者拼接字符串批量删除
            //在后续添加回收站功能改为伪删除后可能就不会因为外键出现bug
            if (ModelState.IsValid)
            {
                //var versionManager = new BLL.HomeworkArticleManager();
                //var articleManager = new BLL.HomeworkArticleManager();
                //foreach (var articleId in model.ArticleIds)
                //{
                //    await articleManager.TrueRemoveAllCommentsByArticleIdAsync(articleId);
                //}
                //删除某个类别所有文章的评论用
                //await articleManager.TrueRemoveAllArticlesByVersionIdAsync(model.Id);
                //await versionManager.TrueRemoveOneVersionAsync(model.Id);
                await new BLL.HomeworkArticleManager().TrueRemoveOneVersionAsync(model.Id);
                return Content("<script>alert('删除成功');window.location.href='/Homework/VersionList';</script>");
            }
            return View(model);
        }

    }
}