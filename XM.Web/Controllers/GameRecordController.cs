using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using XM.Comm;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    public class GameRecordController : BaseController
    {
        [PermissionFilter]
        // GET: GameRecord
        public ActionResult Index()
        {
            return View();
        }
        [PermissionFilter("GameRecord", "Index")]
        public ActionResult GetRecordCollect()
        {
            string action = "GetRecordCollect";

            string starttime = Request["starttime"] == null ? "2019-05-01" : Request["starttime"];
            string endtime = Request["endtime"] == null ? DateTime.Now.Date.ToString() : Request["endtime"];
            string vipAccount = Request["vipAccount"] == null ? "" : Request["vipAccount"]; ;

            string[] paras = { vipAccount, starttime, endtime };
            string key = Md5.GetMd5(paras[0] + paras[1] + paras[2] + KEY);
            string param = GameReturn(action, key, paras);
            var result = HttpPost(param);
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
        [PermissionFilter("GameRecord", "Index")]
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
            string key = Md5.GetMd5(paras[0] + paras[1] + paras[2] + paras[3] + paras[4] + paras[5] + paras[6] + KEY);

            string param = GameReturn(action, key, paras);

            var result = HttpPost(param);
            GameRecord game = JsonConvert.DeserializeObject<GameRecord>(value: result);
            if (game.result != null)
            {
                return PagerData(game.result.total, game.result.data, game.result.pageNum, game.result.pageSize);
            }
            else
            {
                return Content("没有查询到相关数据！");
            }
            
        }
        
    }
}