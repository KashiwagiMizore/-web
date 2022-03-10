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
    public class NoticeController : Controller
    {
        // GET: Notice
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> NoticeList()
        {
            var isAdmin = bool.Parse(Session["isAdmin"].ToString());//获取当前用户的权限
            ViewBag.isAdmin = isAdmin;
            return View(await new BLL.NoticeManager().GetAllNoticesOrderAsync());
        }
        [HttpGet]
        public async Task<ActionResult> NoticeDetails(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(NoticeList));
            }
            return View(await new BLL.NoticeManager().GetOneNoticeByNoticeIdAsync(id.Value));
        }
        [HttpGet]
        [AdminAuth]
        public async Task<ActionResult> EditNotice(Guid? id)
        {
            if (id==null)
            {
                return RedirectToAction(nameof(NoticeList));
            }
            var noticeData =await new BLL.NoticeManager().GetOneNoticeByNoticeIdAsync(id.Value);
            return View(new Models.NoticeViewModels.EditNoticeViewModel()
            {
                Id = noticeData.Id,
                Title = noticeData.Title,
                Content = noticeData.Content
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuth]
        public async Task<ActionResult> EditNotice(Models.NoticeViewModels.EditNoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var noticeTemp =new BLL.NoticeManager();
                await noticeTemp.ChangeNoticeAsync(model.Id, model.Title, model.Content);
                return Content("<script>alert('修改成功');window.location.href='/Notice/NoticeList';</script>");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        [AdminAuth]
        public ActionResult DeleteNotice()
        {
            return View();
        }
        [HttpPost]
        [AdminAuth]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteNotice(Models.NoticeViewModels.DeleteNoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var noticeTemp = new BLL.NoticeManager();
                await noticeTemp.TrueRemoveOneNoticeAsync(model.Id);
                return Content("<script>alert('删除成功');window.location.href='/Notice/NoticeList';</script>");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        [AdminAuth]
        public ActionResult AddNotice()
        {
            return View();
        }
        [HttpPost]
        [AdminAuth]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> AddNotice(Models.NoticeViewModels.AddNoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(Session["userId"].ToString());
                await new BLL.NoticeManager().AddNoticeAsync(model.Title, model.Content, userId);
                return Content("<script>alert('添加成功');window.location.href='/Notice/NoticeList';</script>");
            }
            else
            {
                return View(model);
            }
        }
    }
}