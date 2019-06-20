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
using XM.Model;
using XM.WebVIP.Controllers;
using YMOA.MongoDB;

namespace XM.WebVip.Controllers
{
    /// <summary>
    /// 公告
    /// </summary>
    public class NoticeController : BaseController
    {
        #region view
        /// <summary>
        /// 返回公共发布页
        /// </summary>
        /// <returns></returns>
        public ActionResult Notice()
        {
            return View("_Notice");
        }
        #endregion

        #region Notice
        /// <summary>
        ///  获取公告
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNotice()
        {
            DateTime dtNow = DateTime.Now;
            //判断是否有登录用户
            if (Session["AN"] != null)
            {
                var msgStatus = DALUtility.MDbS.List<NoticState>("XMShop", "noticstate", x => x.uid.Equals(AN) && x.state < 2, x => new NoticState() { msgid = x.msgid }, null);
                var result = DALUtility.MDbS.List<NoticEntity>("XMShop", "notic", x => x.starttime < dtNow && x.endtime > dtNow && (x.receiver == null || x.receiver.Contains(Agent_Acc)) && (x.receivermember == null || x.receivermember.Contains(AN)), null,null);
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
        #endregion
    }
}