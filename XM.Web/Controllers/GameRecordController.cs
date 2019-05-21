﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Comm;

namespace XM.Web.Controllers
{
    public class GameRecordController : BaseController
    {
        // GET: GameRecord
        public ActionResult Index()
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
            string key = Md5.GetMd5(paras[0] + paras[1] + paras[2] + paras[3] + paras[4] + paras[5] + paras[6] + KEY);

            string param = GameReturn(action, key, paras);

            var result = HttpPost("http://172.16.31.232:9678/take", param);
            return Content(result);
        }
    }
}