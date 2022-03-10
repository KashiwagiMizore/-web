using HomeworkSubmit.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeworkSubmit.MVC.Controllers
{
    [LoginAuth]//要先过滤登陆权限 否则会报错
    [AdminAuth]//整个控制器都需要管理员权限

    public class ClassAndUserController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> ClassList()
        {
            return View(await new BLL.UserManager().GetAllClassOrderAsync());
        }
        [HttpGet]
        public async Task<ActionResult> UserList(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(ClassList));
            }
            var users = await new BLL.UserManager().GetAllUserByClassNumIdAsync(id.Value);
            ViewBag.ClassNumId = id.Value;
            var classNum = await new BLL.UserManager().GetClassByClassNumIdAsync(id.Value);
            ViewBag.ClassNum = classNum.ClassNum;
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> UserDetails(string loginName)
        {
            if (loginName == null)
            {
                return RedirectToAction(nameof(UserList));
            }
            return View(await new BLL.UserManager().GetUserByLoginNameAsync(loginName));
        }

        [HttpGet]
        public async Task<ActionResult> EditUser(string loginName)
        {
            if (loginName == null)
            {
                return RedirectToAction(nameof(UserList));
            }
            var userData = await new BLL.UserManager().GetUserByLoginNameAsync(loginName);
            return View(new Models.ClassAndUserViewModels.EditUserViewModel()
            {
                Id = userData.Id,
                Password = userData.Password,
                Name = userData.Name,
                ClassNum = userData.ClassNum,
                IsStudent = userData.IsStudent,
                IsTeacher = userData.IsTeacher
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(Models.ClassAndUserViewModels.EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                var isAdmin = bool.Parse(Session["isAdmin"].ToString());
                var classNumId = await new BLL.UserManager().GetClassIdByClassNumAsync(model.ClassNum);
                if (await userManager.ExistClassNumAsync(model.ClassNum) == false)
                {
                    return Content("<script>alert('不存在该班级');window.location.href='/ClassAndUser/UserList';</script>");
                }
                if (model.IsStudent == model.IsTeacher)
                {
                    return Content("<script>alert('权限设置错误');window.location.href='/ClassAndUser/UserList';</script>");
                }
                await userManager.AdminChangeUserInformationAsync(isAdmin, model.Id, model.Name
                    , model.Password, classNumId, model.IsStudent, model.IsTeacher, model.ClassNum);
                return Content("<script>alert('修改成功');window.location.href='/ClassAndUser/UserList';</script>");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public async Task<ActionResult> EditClass(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(ClassList));
            }
            var classData = await new BLL.UserManager().GetClassByClassNumIdAsync(id.Value);
            return View(new Models.ClassAndUserViewModels.EditClassViewModel()
            {
                Id = classData.ClassNumId,//把DTO中的值赋到ViewModel中
                ClassNum = classData.ClassNum
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditClass(Models.ClassAndUserViewModels.EditClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                if (await userManager.ExistClassNumAsync(model.ClassNum) == true)
                {
                    return Content("<script>alert('该班级已存在');window.location.href='/ClassAndUser/ClassList';</script>");
                }
                await userManager.ChangeClassNumAsync(model.Id, model.ClassNum);
                return Content("<script>alert('修改成功');window.location.href='/ClassAndUser/ClassList';</script>");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult AddTeacher(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(UserList));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTeacher(Models.ClassAndUserViewModels.AddTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                if (await userManager.ExistUserAsync(model.LoginName))
                {
                    return Content("<script>alert('用户已存在');window.location.href='/ClassAndUser/UserList';</script>");
                }
                //model中的Id即get中的id 原理暂时未知 猜测虽然是变量不同 但是get获取到的id在post时会自动给viewmodel中的Id
                await userManager.AddTeacherAsync(model.LoginName, model.Password, model.Name, model.Id);
                return Content("<script>alert('添加成功');window.location.href='/ClassAndUser/UserList';</script>");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult DeleteUser(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(UserList));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(Models.ClassAndUserViewModels.DeleteUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                if (await userManager.ExistAnyArticlesByUserIdAsync(model.Id))
                {
                    return Content("<script>alert('存在由该用户发表的作业 无法删除用户');" +
                        "window.location.href='/ClassAndUser/UserList';</script>");
                }
                if (await userManager.ExistAnyVersionsByUserIdAsync(model.Id))
                {
                    return Content("<script>alert('存在由该用户创建的“第几次作业”目录 无法删除用户');" +
                        "window.location.href='/ClassAndUser/UserList';</script>");
                }
                await userManager.TrueRemoveUserByUserIdAsync(model.Id);
                return Content("<script>alert('删除成功');window.location.href='/ClassAndUser/UserList';</script>");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddClass(Models.ClassAndUserViewModels.AddClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                if (await userManager.ExistClassNumAsync(model.ClassNum) == true)
                {
                    return Content("<script>alert('该班级已存在');window.location.href='/ClassAndUser/ClassList';</script>");
                }
                await userManager.AddClassAsync(model.ClassNum);
                return Content("<script>alert('添加成功');window.location.href='/ClassAndUser/ClassList';</script>");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult DeleteClass(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(ClassList));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteClass(Models.ClassAndUserViewModels.DeleteClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                if (await userManager.ExistAnyUsersInThisClassAsync(model.Id))
                {
                    return Content("<script>alert('班级中存在用户 无法删除');" +
                        "window.location.href='/ClassAndUser/ClassList';</script>");
                }
                await userManager.TrueRemoveClassByClassNumIdAsync(model.Id);
                return Content("<script>alert('删除成功');window.location.href='/ClassAndUser/ClassList';</script>");
            }
            else
            {
                return View(model);
            }
        }
    }
}