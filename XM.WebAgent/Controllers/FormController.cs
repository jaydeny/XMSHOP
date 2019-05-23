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
        public ActionResult ReportsForm()
        {
            return View();
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4/29
        /// 修改时间：2019-
        /// 功能：查询日期,总营业额
        /// </summary>
        public ActionResult QryDayTotal()
        {
            string year = Request["year"];

            string startMonth = Request["startMonth"];
            string endMonth = Request["endMonth"];
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("year", year == null ? DateTime.Now.Year.ToString() : year);
            param.Add("startMonth", startMonth == null ? DateTime.Now.Month.ToString() : startMonth);
            param.Add("endMonth", endMonth == null ? DateTime.Now.Month.ToString() : endMonth);
            param.Add("startDay", Request["startDay"] == null ? "1" : Request["startDay"]);
            param.Add("endDay", Request["endDay"] == null ? "31" : Request["endDay"]);
            param.Add("agent_AN", Session["agent_AN"].ToString());
            //param.Add("agent_AN", Request["agent_AN"]);
            //param.Add("agent_AN", "agent");

            return Content(DALUtility.Agent.QryDayTotal(param));
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:查询日期内的记录
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryDayForm()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("pi", Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]));
            param.Add("pageSize", Request["rows"] == null ? 20 : Convert.ToInt32(Request["rows"]));
            param.Add("sort", Request["sort"] == null ? "id" : Request["sort"]);
            param.Add("order", Request["order"] == null ? "asc" : Request["order"]);

            param.Add("day", Request["day"]);
            param.Add("vip_AN", Request["vip_AN"]);
            param.Add("agent_AN", Session["agent_AN"].ToString());
            //param.Add("agent_AN", Request["agent_AN"]);

            return Content(DALUtility.Agent.QryDayForm(param, out int iCount));
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/5/10
        /// 功能:查询每一笔订单的详细详细
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryDetailOrder()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", Request["order_id"]);
            
            return Content(DALUtility.Agent.QryDetailOrder(param));
        }
        #endregion

        #region _RechargeForm

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-10
        /// 修改时间：2019-
        /// 功能：查询充值
        /// </summary>
        public ActionResult QryDayRechargeTotal()
        {
            int monthDay = DateTime.DaysInMonth(int.Parse(Request["year"]), int.Parse(Request["month"]));

            string startDay = new DateTime(int.Parse(Request["year"]), int.Parse(Request["month"]), 1).Day.ToString();
            string endDay = new DateTime(int.Parse(Request["year"]), int.Parse(Request["month"]), monthDay).Day.ToString();


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("year", Request["year"] == null ? DateTime.Now.Year.ToString() : Request["year"]);
            param.Add("month", Request["month"] == null ? DateTime.Now.Month.ToString() : Request["month"]);
            param.Add("startDay", Request["startDay"] == null ? startDay : Request["startDay"]);
            param.Add("endDay", Request["endDay"] == null ? endDay : Request["endDay"]);
            param.Add("agent_id", Session["agent_ID"].ToString());
            //param.Add("agent_id", Request["agent_id"]);

            return Content(DALUtility.Agent.QryDayRechargeTotal(param));
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:查询日期内的记录
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryDayRechargeForm()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("pi", Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]));
            param.Add("pageSize", Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]));
            param.Add("sort", Request["sort"] == null ? "recharge_id" : Request["sort"]);
            param.Add("order", Request["order"] == null ? "asc" : Request["order"]);

            param.Add("day", Request["day"]);
            param.Add("vip_id", Request["vip_id"]);
            param.Add("agent_id", Session["agent_ID"].ToString());
            //param.Add("agent_id", Request["agent_id"]);

            return Content(DALUtility.Agent.QryDayRechargeForm(param, out int iCount));
        }
        #endregion
    }
}