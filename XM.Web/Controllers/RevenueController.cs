using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    public class RevenueController : BaseController
    {
        [PermissionFilter]
        // GET: Revenue
        public ActionResult Index()
        {
            return View(); 
        }
        [PermissionFilter("Revenue", "Index")]
        public ActionResult GetRechargeRevenue()
        {
            string sort = Request["sort"] == null ? "RechargeID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string startTime = Request["startTime"] == null ? "" : Request["startTime"];
            string endTime = Request["endTime"] == null ? "" : Request["endTime"];
            int AgentID = Request["agent_id"] == null ? 1 : Convert.ToInt32(Request["agent_id"]);
            int VipID = Request["vip_id"] == null ? 1 : Convert.ToInt32(Request["vip_id"]);

            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["AgentID"] = AgentID;
            paras["VipID"] = VipID;
            paras["startTime"] = startTime;
            paras["endTime"] = endTime;
            paras["sort"] = sort;
            paras["order"] = order;
            var charge = DALUtility.Recharge.QryRecharge<RechargeEntity>(paras, out totalCount);
            return PagerData(totalCount, charge);
        }
        
        public ActionResult GetRevenueGoods()
        {
            return View();
        }
        [PermissionFilter("Revenue", "GetGoodsRevenue")]
        public ActionResult GetGoodsRevenue()
        {
            string sort = Request["sort"] == null ? "OrderID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string startTime = Request["startTime"] == null ? "" : Request["startTime"];
            string endTime = Request["endTime"] == null ? "" : Request["endTime"];
            string agentAccountName = Request["agent_AN"] == null ? "" : Request["agent_AN"];


            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["agent_AN"] = agentAccountName;
            paras["startTime"] = startTime;
            paras["endTime"] = endTime;
            paras["sort"] = sort;
            paras["order"] = order;
            var goods = DALUtility.Order.QryOrder<OrderEntity>(paras, out totalCount);
            return PagerData(totalCount, goods);
        }
    }
}