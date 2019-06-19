/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using XM.DALFactory;
using XM.Model;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 基础Controller
    /// </summary>
    public class BaseController : Controller
    {
        #region 登录验证
        /// <summary>
        /// 设置拦截器进行登录验证
        /// </summary>
        /// <param name="filterContext"></param>
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
        #endregion

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
