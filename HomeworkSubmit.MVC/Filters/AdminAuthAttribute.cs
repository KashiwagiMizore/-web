using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeworkSubmit.MVC.Filters
{
    public class AdminAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);
            //只有管理员可以进行 学生教师操作会报错
            if (filterContext.HttpContext.Session["isAdmin"].ToString() == "False")//F要大写 否则会失效 可在浏览器中查看cookie校验
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