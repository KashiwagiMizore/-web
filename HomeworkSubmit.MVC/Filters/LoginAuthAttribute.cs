﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeworkSubmit.MVC.Filters
{
    public class LoginAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);
            //当用户数据在cookie中 session中没有的时候 放入session中
            //此操作过于危险 如果cookie中被修改就会很危险 之后进行修改
            //if (filterContext.HttpContext.Request.Cookies["loginName"]!=null
            //    &&filterContext.HttpContext.Session["loginName"]==null)
            //{
            //    filterContext.HttpContext.Session["loginName"] = filterContext.HttpContext.Request
            //        .Cookies["loginName"].Value;
            //    filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request
            //        .Cookies["userId"].Value;
            //    filterContext.HttpContext.Session["isTeacher"] = filterContext.HttpContext.Request
            //        .Cookies["isTeacher"].Value;
            //}
          
            //验证是否有登陆
            if (!(filterContext.HttpContext.Session["loginName"] != null
                || filterContext.HttpContext.Session["isTeacher"] != null
                || filterContext.HttpContext.Session["userId"] != null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller","Home" },
                    {"action","Login" }
                });
            }
        }
    }
}