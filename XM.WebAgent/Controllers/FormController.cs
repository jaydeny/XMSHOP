using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-5/5
    /// 修改时间：2019-
    /// 功能：报表相关方法.跳转页面等
    /// </summary>
    public class FormController : BaseController
    {
        #region _Form
        /// <summary>
        /// 作者:梁钧淋
        /// 日期:2019/4/29
        /// 功能:返回报表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportForm()
        {
            return View();
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:查询时段内的报表
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryOrder()
        {
            string sort = Request["sort"] == null ? "a.order_date" : Request["sort"];
            string order = Request["order"] == null ? "desc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 20 : Convert.ToInt32(Request["rows"]);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("order", order);
            param.Add("agent_AN", Session["Agent_AN"].ToString());
            if (Session[" AN"] != null)
            {
                param.Add("vip_AN", Session[" AN"].ToString());
            }
            return Content(DALUtility.Vip.QryOrder(param, out int iCount));
        }
        #endregion
    }
}