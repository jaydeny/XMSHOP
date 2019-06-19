/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System;
using System.Web.Mvc;
using XM.Comm;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    /// <summary>
    /// 游戏记录
    /// </summary>
    public class GameRecordController : BaseController
    {
        #region view
        /// <summary>
        /// 返回游戏记录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RecordPage()
        {
            DateTime dt = DateTime.Now;
            //获取一周的开始日期
            string StartWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).ToString("yyyy-MM-dd");
            //获取本周星期天日期
            string EndWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).AddDays(6).ToString("yyyy-MM-dd"); 
            
            ViewData["StartWeek"] = StartWeek;
            ViewData["EndWeek"] = EndWeek; 
            return View();
        }
        /// <summary>
        /// 返回游戏详细记录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailPage()
        {
            return View();
        }
        #endregion

        #region Record
        /// <summary>
        /// 查询时间段内的游戏记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Record()
        {
            //获取前端传过来的数据
            //游戏名,开始时间,结束时间,没有则为空
            string StartDate = Request["StartDate"] == null ? "" : Request["StartDate"];
            string EndDate = Request["EndDate"] == null ? "" : Request["EndDate"];

            string action = "GetRecordCollect";
            string strKey = Md5.GetMd5(AN + StartDate + EndDate + GameUtil.KEY);
            string[] paras = { AN, StartDate, EndDate };

            string param = GameReturn(action, strKey, paras);
            var result = GameUtil.HttpPost(param);

            return Content(result);
        }
        /// <summary>
        /// 返回游戏详细记录数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            //获取前端数据
            //分页所需
            string PIndex = Request["PIndex"] == null ? "1" : Request["PIndex"];
            string PSize = Request["PSize"] == null ? "10" : Request["PSize"];

            //游戏id
            string GameID = Request["GameID"] == null ? "" : Request["GameID"];

            //获取前端传过来的数据
            //游戏名,开始时间,结束时间,没有则为空
            string StartDate = Request["StartDate"] == null ? "" : Request["StartDate"];
            string EndDate = (DateTime.Parse(Request["EndDate"]).AddHours(23).AddMinutes(59)).ToString() == null ? "" : (DateTime.Parse(Request["EndDate"]).AddHours(23).AddMinutes(59)).ToString();

            string action = "GetRecord";
            string strKey = Md5.GetMd5(AN + GameID + StartDate + EndDate + PIndex + "" + PSize + GameUtil.KEY);
            string[] paras = { AN, GameID, StartDate, EndDate, PIndex, "", PSize };

            string param = GameReturn(action, strKey, paras);
            var result = GameUtil.HttpPost(param);
            return Content(result);
        }
        #endregion
    }
}