using System;
using System.Web.Mvc;
using XM.Comm;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    public class GameRecordController : BaseController
    {
        // GET: GameRecord
        public ActionResult RecordPage()
        {
            DateTime dt = DateTime.Now;
            string StartWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).ToString("yyyy-MM-dd"); //获取一周的开始日期
            string EndWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).AddDays(6).ToString("yyyy-MM-dd"); //获取本周星期天日期
            ViewData["StartWeek"] = StartWeek;
            ViewData["EndWeek"] = EndWeek; 
            return View();
        }

        public ActionResult Record()
        {
            //获取前端传过来的数据
            //游戏名,开始时间,结束时间,没有则为空
            string StartDate = Request["StartDate"] == null ? "" : Request["StartDate"];
            string EndDate = Request["EndDate"] == null ? "" : Request["EndDate"];

            string action = "GetRecordCollect";
            string strKey = Md5.GetMd5(AN + StartDate + EndDate + KEY);
            string[] paras = { AN, StartDate, EndDate };

            string param = GameReturn(action, strKey, paras);
            var result = HttpPost(param);

            return Content(result);
        }


        // GET: GameRecord
        public ActionResult DetailPage()
        {
            return View();
        }

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
            string EndDate = Request["EndDate"] == null ? "" : Request["EndDate"];

            string action = "GetRecord";
            string strKey = Md5.GetMd5(AN + GameID + StartDate + EndDate + PIndex + "" + PSize + KEY);
            string[] paras = { AN, GameID, StartDate, EndDate, PIndex, "", PSize };

            string param = GameReturn(action, strKey, paras);
            var result = HttpPost(param);
            return Content(result);
        }

        public ActionResult Detail1()
        {
            //获取前端数据
            //分页所需
            string PIndex = "2";
            string PSize = "10";

            //游戏id
            string GameID = "2";

            //获取前端传过来的数据
            //游戏名,开始时间,结束时间,没有则为空
            string StartDate = "2019-5-20";
            string EndDate = "2019-5-26";

            string action = "GetRecord";
            string strKey = Md5.GetMd5("vip00" + GameID + StartDate + EndDate + PIndex + "" + PSize + KEY);
            string[] paras = { "vip00", GameID, StartDate, EndDate, PIndex, "", PSize };

            string param = GameReturn(action, strKey, paras);
            var result = HttpPost(param);
            return Content(result);
        }
    }
}