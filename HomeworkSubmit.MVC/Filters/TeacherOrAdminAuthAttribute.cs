using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeworkSubmit.MVC.Filters
{
    public class TeacherOrAdminAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);
            //用于只有教师或管理员能执行的操作 过滤学生操作 学生操作即报错
            if (filterContext.HttpContext.Session["isTeacher"].ToString() == "False"
                && filterContext.HttpContext.Session["isAdmin"].ToString() == "False"
                )//F要大写 否则会失效 可在浏览器中查看cookie校验
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller","Home" },
                    {"action","ErrorMessage" }
                });
            }
        }
    }
}