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
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  获取公告
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNotice()
        {
            DateTime dtNow = DateTime.Now;
            List<NoticEntity> result = null;
            if (AN != null)
            {
                var msgStatus = DALUtility.MDbS.List<NoticState>("YMOA", "msg_state", x => x.uid.Equals(AN) && x.state < 2, x => new NoticState() { msgid = x.msgid }, null);
                List<string> listMsgId = new List<string>();
                foreach (var ms in msgStatus)
                {
                    listMsgId.Add(ms.msgid);
                }
                result = DALUtility.MDbS.List<NoticEntity>("YMOA", "msg", x => x.starttime < dtNow && x.endtime > dtNow && (x.receiver == null || x.receiver.Contains(Agent_ID)) && !listMsgId.Contains(x._id),null,null);
            }
            else
            {
                result = DALUtility.MDbS.List<NoticEntity>("YMOA", "msg", x => x.starttime < dtNow && x.endtime > dtNow && x.receiver == null, null, null);
            }
            return PagerData(result.Count,result) ;
        }

    }
}