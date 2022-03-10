using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeworkSubmit.MVC.Filters
{
    public class RememberPwdAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);
            //给必要的cookie一个初始值 用于记住密码功能用（非自动登录）
            if (filterContext.HttpContext.Request.Cookies["rememberMe"] == null)
            {
                filterContext.HttpContext.Response.Cookies.Add(new HttpCookie("rememberMe")
                {
                    Value = false.ToString(),
                    Expires = DateTime.Now.AddDays(7)
                });
            }
            if (filterContext.HttpContext.Request.Cookies["loginName"] == null)
            {
                filterContext.HttpContext.Response.Cookies.Add(new HttpCookie("loginName")
                {
                    Value = "default",
                    Expires = DateTime.Now.AddDays(7)
                });
            }
            if (filterContext.HttpContext.Request.Cookies["password"] == null)
            {
                filterContext.HttpContext.Response.Cookies.Add(new HttpCookie("password")
                {
                    Value = "default",
                    Expires = DateTime.Now.AddDays(7)
                });
            }
        }

    }
}