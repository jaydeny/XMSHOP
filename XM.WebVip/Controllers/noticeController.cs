using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;
using YMOA.MongoDB;

namespace XM.WebVip.Controllers
{
    public class NoticeController : BaseController
    {
        // GET: notice
        public ActionResult Notice()
        {
            ViewData["AN"] = Session["AN"];
            return View("_Notice");
        }

        /// <summary>
        ///  获取公告
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNotice()
        {
            DateTime dtNow = DateTime.Now;
            List<NoticEntity> result = null;
            if (Session["AN"] != null)
            {
                var msgStatus = DALUtility.MDbS.List<NoticState>("XMShop", "noticstate", x => x.uid.Equals(AN) && x.state < 2, x => new NoticState() { msgid = x.msgid }, null);
                List<string> listMsgId = new List<string>();
                foreach (var ms in msgStatus)
                {
                    listMsgId.Add(ms.msgid);
                }
                result = DALUtility.MDbS.List<NoticEntity>("XMShop", "notic", x => x.starttime < dtNow && x.endtime > dtNow && (x.receiver == null || x.receiver.Contains(Agent_ID)) && !listMsgId.Contains(x._id),null,null);
            }
            else
            {
                MongoDbService dbService = new MongoDbService();
                result = dbService.List<NoticEntity>("XMShop", "notic", x => x.starttime < dtNow && x.endtime > dtNow && x.receiver == null, null, null);
            }
            return PagerData(result.Count,result);
        }

        /// <summary>
        ///  添加已读公告
        /// </summary>
        /// <param name="msgid"></param>
        /// <returns></returns>
        public ActionResult AddNotice(string msgid)
        {
            DALUtility.MDbS.Add<NoticState>("XMShop", "noticstate", new NoticState() { uid = AN, msgid = msgid, state = 1 });
            var result = DALUtility.MDbS.List<NoticState>("XMShop", "noticstate", x => x.uid.Equals(AN) && x.msgid.Equals(msgid), x => new NoticState() { msgid = x.msgid }, null);
            return OperationReturn(result.Count>0, "");
        }

    }
}