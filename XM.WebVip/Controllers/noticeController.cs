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
            return View("_Notice");
        }

        /// <summary>
        ///  获取公告
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNotice()
        {
            DateTime dtNow = DateTime.Now;
            if (Session["AN"] != null)
            {
                var msgStatus = DALUtility.MDbS.List<NoticState>("XMShop", "noticstate", x => x.uid.Equals(AN) && x.state < 2, x => new NoticState() { msgid = x.msgid }, null);
                var result = DALUtility.MDbS.List<NoticEntity>("XMShop", "notic", x => x.starttime < dtNow && x.endtime > dtNow && (x.receiver == null || x.receiver.Contains(Agent_ID)),null,null);
                return PagerData(-1, new { msgStatus,result});
            }
            else
            {
                MongoDbService dbService = new MongoDbService();
                var result = dbService.List<NoticEntity>("XMShop", "notic", x => x.starttime < dtNow && x.endtime > dtNow && x.receiver == null, null, null);
                return PagerData(result.Count, result);
            }
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