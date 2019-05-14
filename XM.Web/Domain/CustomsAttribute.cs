/*-------------------------------------*
 * 創建人:         J.Y
 * 創建時間:       2019/04/28
 * 最后修改時間:    
 * 最后修改原因:
 * 修改歷史:
 * 2019/04/28       J.Y       創建
 *-------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;

namespace XM.Web.Domain
{
    /// <summary>
    /// 权限拦截过滤
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionFilterAttribute : ActionFilterAttribute
    {
        private string controller { get; set; }
        private string action { get; set; }
        private Operationype operationype { get; set; }
        private bool isViewPage = false;


        /// <summary>
        /// 权限过滤
        /// </summary>
        /// <param name="_controller">Controller</param>
        /// <param name="_action">Action</param>
        /// <param name="_operationype">执行动作类型</param>
        public PermissionFilterAttribute(string _controller = "", string _action = "", Operationype _operationype = Operationype.View)
        {
            isViewPage = _controller.Equals(string.Empty) && _action.Equals(string.Empty);
            controller = _controller;
            action = _action;
            operationype = _operationype;
        }
        /// <summary>
        /// 权限拦截
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var allowAccess = false;
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (controller.Equals(string.Empty))
            {
                controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            }
            if (action.Equals(string.Empty))
            {
                action = filterContext.RouteData.Values["action"].ToString().ToLower();
            }
            if (HttpContext.Current.Session["RoleMenu"] != null)
            {
                var memuInfo = ((IEnumerable<Navbar>)HttpContext.Current.Session["RoleMenu"]).SingleOrDefault(
                    x => x.Controller.Equals(controller, StringComparison.CurrentCultureIgnoreCase)
                    && x.Action.Equals(action, StringComparison.CurrentCultureIgnoreCase));
                if (memuInfo != null)
                {
                    switch (operationype)
                    {
                        case Operationype.View:
                            allowAccess = true;
                            break;
                        case Operationype.Add:
                            allowAccess = memuInfo.RmAdd;
                            break;
                        case Operationype.Update:
                            allowAccess = memuInfo.RmUpdate;
                            break;
                        case Operationype.Delete:
                            allowAccess = memuInfo.RmDelete;
                            break;
                        case Operationype.Other:
                            allowAccess = memuInfo.RmOther;
                            break;
                    }
                    filterContext.Controller.ViewData["Add"] = memuInfo.RmAdd;
                    filterContext.Controller.ViewData["Update"] = memuInfo.RmUpdate;
                    filterContext.Controller.ViewData["Delete"] = memuInfo.RmDelete;
                    filterContext.Controller.ViewBag.Title = memuInfo.nameOption;
                }
            }

            if (!allowAccess)
            {
                filterContext.HttpContext.Session.Clear();
                //filterContext.HttpContext.Request.IsAjaxRequest
                if (isViewPage)
                {
                    filterContext.RequestContext.HttpContext.Response.Redirect("/Login");
                }
                else
                {
                    filterContext.Result = new ContentResult() { Content = "-100" };
                }
            }
            else
            {
                //判断重复登入
                Hashtable htOnline = (Hashtable)System.Web.HttpContext.Current.Application["CurrentOnline"];
                if (htOnline != null && htOnline[filterContext.HttpContext.Session["User"].ToString()] != filterContext.HttpContext.Session["LoginTime"])
                {
                    filterContext.HttpContext.Session.Clear();
                    if (isViewPage)
                    {
                        filterContext.RequestContext.HttpContext.Response.Redirect("/Login?t=rl");
                    }
                    else
                    {
                        filterContext.Result = new ContentResult() { Content = "-101" };
                    }
                    return;
                }
            }
        }
    }
}