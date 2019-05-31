using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XM.Comm;
using XM.DALFactory;
using XM.Model;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 基础Controller
    /// </summary>
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.RouteData.Values["controller"].ToString().Equals("Login"))
            {
                UserEntity uInfo = Session["User"] as UserEntity;
                if (uInfo == null)
                {
                    filterContext.RequestContext.HttpContext.Response.Redirect("/Login");
                }
            }
        }
        #region 数据交互接口
        internal DALCore DALUtility => DALCore.GetInstance();
        #endregion
        #region  分页方法返回（不常用）
        protected ContentResult PagerData(int totalCount, object rows)
        {
            return Content(JsonConvert.SerializeObject(new { total = totalCount.ToString(), rows = rows }));
        }
        #endregion
        #region  分页方法（常用）
        /// <param name="totalCount">总记录数</param>
        /// <param name="rows">数据</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">页面条数</param>
        /// <returns></returns>
        protected ContentResult PagerData(int totalCount, object rows, int page, int pageSize)
        {
            var data = new
            {
                // 数据
                rows = rows,
                // 总页数
                total = (int)Math.Ceiling((double)totalCount / pageSize),
                // 当前页
                page = page,
                // 总记录数
                records = totalCount
            };
            return Content(JsonConvert.SerializeObject(data));
        }
        #endregion
        #region  返回操作信息
        protected ContentResult OperationReturn(bool _success, string _msg = "")
        {
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success }));

        }
        #endregion
    }
    #region  返回结果
    public class result_base
    {
        public string errorCode { get; set; } = "";
        public string errorMsg { get; set; } = "";
        public object result { get; set; }
    }
    #endregion
}
