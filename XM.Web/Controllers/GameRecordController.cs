using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using XM.Comm;
using XM.Model;

namespace XM.Web.Controllers
{
    public class GameRecordController : BaseController
    {
        //private GameUtil gameUtil = new GameUtil();

        // GET: GameRecord
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetRecordCollect()
        {
            string action = "GetRecordCollect";

            string starttime = Request["starttime"] == null ? "2019-05-01" : Request["starttime"];
            string endtime = Request["endtime"] == null ? DateTime.Now.Date.ToString() : Request["endtime"];
            string vipAccount = Request["vipAccount"] == null ? "" : Request["vipAccount"]; ;

            string[] paras = { vipAccount, starttime, endtime };
           var result = DALUtility.Game.ReturnRes(paras,action);
            RecordCollect game = JsonConvert.DeserializeObject<RecordCollect>(value: result);
            var data = new
            {
                errorCode = game.errorCode,
                errorMsg = game.errorMsg,
                rows = game.result
            };
            return Content(JsonConvert.SerializeObject(data));
        }
        public ActionResult GetRecord()
        {
            return View();
        }
        public ActionResult Record()
        {
            string action = "GetRecord";

            string page = Request["page"] == null ? "1" : Request["page"];
            string rows = Request["rows"] == null ? "10" : Request["rows"];
            string vipAccount = Request["vipAccount"] == null ? "" : Request["vipAccount"];
            string agentAccount = Request["agentAccount"] == null ? "" : Request["agentAccount"];
            string starttime = Request["starttime"] == null ? "2019-05-01" : Request["starttime"];
            string endtime = Request["endtime"] == null ? DateTime.Now.Date.ToString() : Request["endtime"];
            string ID = Request["ID"] == null ? "" : Request["ID"];
       
            string[] paras = { vipAccount, ID, starttime, endtime, page, agentAccount, rows };
            var result = DALUtility.Game.ReturnRes(paras, action);
            GameRecord game = JsonConvert.DeserializeObject<GameRecord>(value: result);
            return PagerData(game.result.total, game.result.data, game.result.pageNum, game.result.pageSize);
        }
        
    }
}