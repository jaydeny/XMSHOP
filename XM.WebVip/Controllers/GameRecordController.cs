using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Comm;
using XM.Web.Controllers;

namespace XM.WebVip.Controllers
{
    public class GameRecordController : BaseController
    {
        // GET: GameRecord
        public ActionResult RecordPage()
        {
            //获取前端传过来的数据
            //游戏名,开始时间,结束时间,没有则为空
            string GameName = Request["GameName"] == null ? "null" : Request["GameName"];
            string StartDate = Request["StartDate"] == null ? "null" : Request["StartDate"];
            string EndDate = Request["EndDate"] == null ? "null" : Request["EndDate"];

            string action = "GetRecordCollect";
            string strKey = Md5.GetMd5(AN + GameName + StartDate + EndDate + KEY);
            string[] paras = { AN,GameName,StartDate,EndDate};

            string param = GameReturn(action,strKey,paras);
            return View(); 
        }

        public ActionResult Record()
        {

            return View();
        }


        // GET: GameRecord
        public ActionResult DetailPage()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}