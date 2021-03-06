﻿/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{
    /// <summary>
    /// 功能：充值审核，查询
    /// </summary>
    public class ExamineController : BaseController
    {
        #region _view
        /// <summary>
        /// 返回审核视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region _RechargeForm

        /// <summary>
        /// 功能：查询充值
        /// </summary>
        public ActionResult QryDayRechargeTotal()
        {
            DateTime time = DateTime.Now;
            var data = new DateTime(time.Year, time.Month, 1);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("startTime", Request["startTime"] == null ? data.ToString("yyyy-MM-dd") : Request["startTime"]);
            param.Add("endTime", Request["endTime"] == null ? time.ToString("yyyy-MM-dd") : Request["endTime"]);
            param.Add("status", Request["status"]);
            param.Add("vip_AN", Request["vip_AN"]);
            param.Add("agent_AN", Session["agent_AN"].ToString());
            return Content(DALUtility.Agent.QryDayExamineTotal(param));
        }

        /// <summary>
        /// 功能:查询日期内的记录
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryDayRechargeForm()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("pi", Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]));
            param.Add("pageSize", Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]));
            param.Add("sort", Request["sort"] == null ? "id" : Request["sort"]);
            param.Add("order", Request["order"] == null ? "asc" : Request["order"]);
            param.Add("status_id", Request["status"]);
            param.Add("day", Request["day"]);
            if ("".Equals(Request["vip_id"]))
                param.Add("vip_AN", null);
            else
                param.Add("vip_AN", Request["vip_id"]);
            param.Add("agent_AN", Session["agent_AN"].ToString());
            return Content(DALUtility.Agent.QryDayExamineForm(param, out int iCount));
        }
        /// <summary>
        /// 充值审核动作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RechargeAudit()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", Request["type"]);
            param.Add("recharge_id", Request["id"]);
            param.Add("vip_AN", Request["name"]);
            param.Add("remainder", Request["integral"]);
            var code = DALUtility.Agent.RechargeAudit(param);
            if (code == 1)
                return OperationReturn(true, "审核通过");
            else
                return OperationReturn(false, "审核已回退");
        }
        #endregion

    }
}