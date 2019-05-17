using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 创建人：曾贤鑫
    /// 创建时间：2019/04/26
    /// 报表
    /// </summary>
    public class RevenueController : BaseController
    {
        #region _Form
        public ActionResult ReportForm()
        {
            return View();
        }

        /// <summary>
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
            param.Add("agent_AN", Request["agent_AN"] == null ? "" : Request["agent_AN"] );

            return Content(DALUtility.Agent.QryDayTotals(param));
        }
        public ActionResult GetInfoForm()
        {
            return View();
        }
        /// <summary>
        /// 查询日期内的记录
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryDayForm()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("pi", Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]));
            param.Add("pageSize", Request["rows"] == null ? 20 : Convert.ToInt32(Request["rows"]));
            param.Add("sort", Request["sort"] == null ? "OrderID" : Request["sort"]);
            param.Add("order", Request["order"] == null ? "asc" : Request["order"]);

            param.Add("day", Request["day"]);
            param.Add("vip_AN", Request["vip_AN"]);
            //param.Add("agent_AN", Session["agent_AN"].ToString());
            param.Add("agent_AN", Request["agent_AN"] == null ? "" : Request["agent_AN"]);

            return Content(DALUtility.Agent.QryDayForms(param, out int iCount));
        }
        public ActionResult GetOrderForm()
        {
            return View("_Form");
        }
        /// <summary>
        /// 功能:查询每一笔订单的详细详细
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryDetailOrder()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]));
            param.Add("pageSize", Request["rows"] == null ? 20 : Convert.ToInt32(Request["rows"]));
            param.Add("sort", Request["sort"] == null ? "id" : Request["sort"]);
            param.Add("order", Request["order"] == null ? "asc" : Request["order"]);

            param.Add("id", Request["id"]);

            return Content(DALUtility.Agent.QryDetailOrder(param));
        }
        #endregion

        #region _RechargeForm
        public ActionResult RechargeForm()
        {
            return View();
        }

        /// <summary>
        /// 功能：查询充值
        /// </summary>
        public ActionResult QryDayRechargeTotal()
        {
            string year = Request["year"];
            string startMonth = Request["startMonth"];
            string endMonth = Request["endMonth"];
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]));
            param.Add("pageSize", Request["rows"] == null ? 20 : Convert.ToInt32(Request["rows"]));
            param.Add("sort", Request["sort"] == null ? "RDate" : Request["sort"]);
            param.Add("order", Request["order"] == null ? "asc" : Request["order"]);

            param.Add("year", year == null ? DateTime.Now.Year.ToString() : year);
            param.Add("startMonth", startMonth == null ? DateTime.Now.Month.ToString() : startMonth);
            param.Add("endMonth", endMonth == null ? DateTime.Now.Month.ToString() : endMonth);
            param.Add("startDay", Request["startDay"] == null ? "1" : Request["startDay"]);
            param.Add("endDay", Request["endDay"] == null ? "31" : Request["endDay"]);
            //param.Add("agent_AN", Session["agent_AN"].ToString());
            param.Add("agent_AN", Request["agent_AN"] == null ? "" : Request["agent_AN"]);

            return Content(DALUtility.Agent.QryDayRechargeTotal(param));
        }

        public ActionResult DayRevenueForm()
        {
            return View();
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
            //param.Add("agent_id", Session["agent_ID"].ToString());
            param.Add("agent_id", Request["agent_id"]);

            return Content(DALUtility.Agent.QryDayRechargeForm(param, out int iCount));
        }
        #endregion
    }
}