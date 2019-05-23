using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{
    /// <summary>
    /// 作者：梁钧淋
    /// 创建时间:2019-5-21
    /// 修改时间：2019-
    /// 功能：发布公告
    /// </summary>
    public class NoticeController : BaseController
    {
        // 返回发布公告主视图
        public ActionResult Index()
        {
            return View();
        }
        //发布公告动作
        public ActionResult ReleaseNotic()
        {
            NoticEntity notic = new NoticEntity();
            notic.publisher = Session["agent_AN"].ToString();
            notic.receiver = Session["agent_AN"].ToString().Split().ToList<string>();
            notic.title = Request["title"];
            notic.content = Request["content"];
            notic.starttime = DateTime.Parse(Request["StartDate"]);
            notic.endtime = DateTime.Parse(Request["EndDate"]);
           
            if (Request["receiver"] != "")
            {
                notic.receivermember = Request["receiver"].Split(',').ToList<string>();
            }

            string NoticID = notic._id;

            DALUtility.MDbS.Add<NoticEntity>("XMShop", "notic", notic);

            var iCheck = DALUtility.MDbS.GetCount<NoticEntity>("XMShop", "notic", x => x._id == NoticID);
            if (iCheck == 0)
            {
                return OperationReturn(false, "发布公告失败!");
            }
            return OperationReturn(true, "发布公告成功!");
        }
    }
}