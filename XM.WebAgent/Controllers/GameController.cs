using System;
using System.Collections.Generic;
using System.Linq;
using XM.Comm;
using System.Web;
using System.Web.Mvc;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{
    /// <summary>
    /// 创建人:梁钧淋
    /// 创建日期:2019-5-17
    /// 修改日期:2019-5-17
    /// 功能: 对游戏端API接入，进行游戏端数据处理，可视化
    /// </summary>
    public class GameController : BaseController
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 游戏报表
        /// </summary>
        /// <returns></returns>
        public ActionResult GameForm() {
            return View();
        }
        /// <summary>
        /// 获取所有游戏
        /// </summary>
        /// <returns></returns>
        public string GetGameOrByAccount() {
            string[] param = {  };
            return  ReturnRes(param, "GetGameOrByAccount");
        }
        /// <summary>
        /// 根据用户名获取游戏记录
        /// </summary>
        /// <returns></returns>
        public string GetRecordCollect()
        {
            string vipName = Request["vipName"] == null ? "" : Request["vipName"];
            string startTime = Request["startTime"] == null ? "" : Request["startTime"];
            string endTime = Request["endTime"] == null ? "" : Request["endTime"];
            string[] param = { vipName,startTime,endTime };
            return ReturnRes(param, "GetRecordCollect");
        }
        /// <summary>
        /// 获取当前时间段（天）的记录数据
        /// </summary>
        /// <returns></returns>
        public string GetRecordCollectByAgency() {

            string ID = Request["ID"] == null ? "" : Request["ID"];
            string startTime = Request["startTime"] == null ? "" : Request["startTime"];
            string endTime = Request["endTime"] == null ? "" : Request["endTime"];
            string[] param = { Session["agent_AN"].ToString(), ID, startTime,endTime };
            return ReturnRes(param, "GetRecordCollectByAgency");
        }
        /// <summary>
        /// 获取当天的游戏记录数据
        /// </summary>
        /// <returns></returns>
        public string GetRecord() {
            string ID = Request["ID"] == null ? "" : Request["ID"];
            string time = Request["time"] == null ? "" : Request["time"];
            string page = Request["page"] == null ? "" : Request["page"];
            string rows = Request["rows"] == null ? "" : Request["rows"];
            string[] param = { "", ID, time, time,page, Session["agent_AN"].ToString(), rows };
            return ReturnRes(param, "GetRecord");
        }
        /// <summary>
        /// 根据记录ID获取详细游戏信息
        /// </summary>
        /// <returns></returns>
        public string GetRecordSpecific()
        {
            string ID = Request["ID"] == null ? "" : Request["ID"];
            string[] param = { ID };
            return ReturnRes(param, "GetRecordSpecific");
        }

    }
}