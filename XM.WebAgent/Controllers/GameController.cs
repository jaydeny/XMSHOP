/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System.Web.Mvc;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{
    /// <summary>
    /// 功能: 对游戏端API接入，进行游戏端数据处理，可视化
    /// </summary>
    public class GameController : BaseController
    {
        #region view
        /// <summary>
        /// 返回代理游戏视图
        /// </summary>
        /// <returns></returns>
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
        #endregion

        #region select
        /// <summary>
        /// 获取所有游戏
        /// </summary>
        /// <returns></returns>
        public string GetGameOrByAccount() {
            string[] param = {  };
            return DALUtility.Game.ReturnRes(param, "GetGameOrByAccount");
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
            return DALUtility.Game.ReturnRes(param, "GetRecordCollect");
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
            return DALUtility.Game.ReturnRes(param, "GetRecordCollectByAgency");
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
            return DALUtility.Game.ReturnRes(param, "GetRecord");
        }
        /// <summary>
        /// 根据记录ID获取详细游戏信息
        /// </summary>
        /// <returns></returns>
        public string GetRecordSpecific()
        {
            string ID = Request["ID"] == null ? "" : Request["ID"];
            string[] param = { ID };
            return DALUtility.Game.ReturnRes(param, "GetRecordSpecific");
        }
        #endregion
    }
}