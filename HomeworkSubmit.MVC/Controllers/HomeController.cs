using HomeworkSubmit.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeworkSubmit.MVC.Controllers
{
    public class HomeController : Controller
    {
        [LoginAuth]
        public ActionResult Index()
        {
            //DateTime timeTest1 = DateTime.Now;
            //DateTime timeTest2 = DateTime.Now.AddMinutes(30);
            //var timeTest = timeTest2 - timeTest1;
            //var test = timeTest.TotalMinutes; //获得时间间隔 test为double类型 用于设置作业截止时间
            return View();
        }
        [LoginAuth]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [LoginAuth]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            //ViewBag.Classes =await new BLL.UserManager().GetAllClassOrderAsync();//必须放在get中用于显示
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.UserViewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                //ViewBag.Classes = await new BLL.UserManager().GetAllClassOrderAsync();//post中用于提交防止出现空值 有BUG
                if (await userManager.ExistClassNumAsync(model.ClassNum) == false)
                {
                    return Content("<script>alert('不存在该班级');window.location.href='/Home/Register';</script>");
                }
                var classNumId = await userManager.GetClassIdByClassNumAsync(model.ClassNum);
                if (await userManager.RegisterAsync(model.LoginName, model.Password, model.ClassNum, model.Name, classNumId))
                {
                    return Content("<script>alert('注册成功');window.location.href='../Home/Login';</script>");
                }
                else
                {
                    ModelState.AddModelError("", "该用户已存在");
                }
                //return Content("注册成功");

            }
            return View(model);
        }
        [HttpGet]
        [RememberPwdAuth]//用于记住密码 在获取页面的时候就要进行操作
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(Models.UserViewModels.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                Guid userId, classNumId;
                bool isTeacher, isAdmin, isStudent;
                int classNum;
                if (userManager.Login(model.LoginName, model.Password,
                    out userId, out isTeacher, out isAdmin, out isStudent, out classNum, out classNumId))
                {
                    if (model.RememberMe)//勾选记住密码
                    {
                        Request.Cookies.Remove("loginName");
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.LoginName,
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Request.Cookies.Remove("password");
                        Response.Cookies.Add(new HttpCookie("password")
                        {
                            Value = model.Password,
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Request.Cookies.Remove("rememberMe");
                        Response.Cookies.Add(new HttpCookie("rememberMe")
                        {
                            Value = true.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie("classNum")
                        {
                            Value = classNum.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Session["loginName"] = model.LoginName;
                        Session["userId"] = userId;
                        Session["isTeacher"] = isTeacher;
                        Session["isAdmin"] = isAdmin;
                        Session["isStudent"] = isStudent;
                        Session["classNum"] = classNum;
                        Session["classNumId"] = classNumId;
                        //Response.Cookies.Add(new HttpCookie("userId")
                        //{
                        //    Value = userId.ToString(),//字符串形式保存
                        //    Expires = DateTime.Now.AddDays(7)
                        //});
                        //Response.Cookies.Add(new HttpCookie("isTeacher")
                        //{
                        //    Value = isTeacher.ToString(),//字符串形式保存
                        //    Expires = DateTime.Now.AddDays(7)
                        //});
                        //过于危险 所以在之后换一个方法
                    }
                    else
                    {
                        //直接删除不可行  得替换或者让其过期
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.LoginName,
                            Expires = DateTime.Now.AddDays(-1)
                        });
                        Response.Cookies.Add(new HttpCookie("password")
                        {
                            Value = model.Password,
                            Expires = DateTime.Now.AddDays(-1)
                        });
                        Response.Cookies.Add(new HttpCookie("rememberMe")
                        {
                            Value = true.ToString(),
                            Expires = DateTime.Now.AddDays(-1)
                        });
                        Response.Cookies.Add(new HttpCookie("classNum")
                        {
                            Value = classNum.ToString(),
                            Expires = DateTime.Now.AddDays(-1)
                        });
                        Session["loginName"] = model.LoginName;
                        Session["userId"] = userId;
                        Session["isTeacher"] = isTeacher;
                        Session["isAdmin"] = isAdmin;
                        Session["isStudent"] = isStudent;
                        Session["classNum"] = classNum;
                        Session["classNumId"] = classNumId;
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "账号密码有误");
                }
            }
            return View(model);
        }

        [HttpGet]
        [LoginAuth]
        public async Task<ActionResult> EditUserInformation()
        {
            var loginName = Session["loginName"].ToString();//获取当前登录用户
            var userData = await new BLL.UserManager().GetUserByLoginNameAsync(loginName);
            return View(new Models.UserViewModels.EditUserInformationViewModel()
            {
                Id = userData.Id,
                Name=userData.Name,
                OldPwd=userData.Password
            });
        }
        [HttpPost]
        [LoginAuth]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserInformation(Models.UserViewModels.EditUserInformationViewModel model)
        {
            var loginName = Session["loginName"].ToString();//获取当前登录用户
            if (ModelState.IsValid)
            {
                var userManager = new BLL.UserManager();
                await userManager.ChangeInformationAsync(loginName, model.OldPwd, model.NewPwd, model.Name);
                return Content("<script>alert('修改成功');window.location.href='/Home/Index';</script>");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ErrorMessage()
        {
            return Content("<script>alert('没有该操作权限');window.location.href='/Home/Index';</script>");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}