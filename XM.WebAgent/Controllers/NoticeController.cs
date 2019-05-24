using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Controllers;
using YMOA.MongoDB.Model;

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
        #region 视图
        // 返回发布公告主视图
        public ActionResult Index()
        {
            return View();
        }
        //公告记录视图
        public ActionResult NoticRecord()
        {
            return View();
        }
        #endregion

        #region 添加
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
        #endregion

        #region 查询
        /// <summary>
        /// 功能:返回当前代理用户的发布公告记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Manager()
        {
            var pageIndex = int.Parse(Request["page"]) != 0 ? int.Parse(Request["page"]) : 1;
            var pageSize = int.Parse(Request["rows"]) != 0 ? int.Parse(Request["rows"]) : 1;
            string agentName = Session["agent_AN"].ToString();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("title", Request["title"]);
            dic.Add("receiver", Request["receiver"]);

            PageList<NoticEntity> pageList = DALUtility.MDbS.PageList<NoticEntity>("XMShop", "notic",
                x => x.receiver == new List<string> { agentName },
                null, pageIndex, pageSize, null, true);

            return PagerData(pageList.Total, pageList.Items, pageList.PageIndex, pageList.PageSize);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 功能:返回当前代理用户的发布公告记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Del_NoticbyID()
        {
            
            var id = Request["id"];
            string agentName = Session["agent_AN"].ToString();  
           var pageList = DALUtility.MDbS.DeleteAsync<NoticEntity>("XMShop", "notic",
                x => x._id == id);

            if (pageList.Result < 0) {
                return Content(JsonConvert.SerializeObject(new { msg = "撤销失败" , success=false }));
            }
            return Content(JsonConvert.SerializeObject(new { msg = "撤销成功", success = true }));
        }
        #endregion
    }
}