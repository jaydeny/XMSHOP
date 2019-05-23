using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using YMOA.MongoDB;

namespace XM.Web.Controllers
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-5-21
    /// 修改时间：2019-
    /// 功能：发布公告
    /// </summary>
    public class NoticController : BaseController
    {
        // GET: Notic
        /// <summary>
        /// 功能:发布公告
        /// </summary>
        /// <returns></returns>
        public ActionResult ReleaseNotic()
        {
            UserEntity user = Session["User"] as UserEntity;

            NoticEntity notic = new NoticEntity();
            notic.publisher = user.UserAccountName;
            notic.title = Request["title"];
            notic.content = Request["content"];
            notic.starttime = DateTime.Parse(Request["StartDate"]);
            notic.endtime = DateTime.Parse(Request["EndDate"]);
            if (Request["receiver"] != "")
            {
                notic.receiver = Request["receiver"].Split(',').ToList<string>();
            }

            string NoticID = notic._id;

            DALUtility.MDbS.Add<NoticEntity>("XMShop", "notic", notic);

            var iCheck = DALUtility.MDbS.GetCount<NoticEntity>("XMShop", "notic", x => x._id == NoticID);
            if(iCheck == 0)
            {
                return OperationReturn(false, "发布公告失败!");
            }
            return OperationReturn(true,"发布公告成功!");
        }

        /// <summary>
        /// 功能:获取所有的代理商,并返回
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllAgent()
        {
            return Content(DALUtility.Notic.GetAllAgent());
        }
    }
}