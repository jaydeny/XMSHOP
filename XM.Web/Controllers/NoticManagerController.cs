using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using YMOA.MongoDB.Model;

namespace XM.Web.Controllers
{
    public class NoticManagerController : BaseController
    {
        // GET: NoticManager
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 功能:返回视图,分页显示公告
        /// </summary>
        /// <returns></returns>
        public ActionResult Manager()
        {
            var pageIndex = int.Parse(Request["page"]) != 0 ? int.Parse(Request["page"])  : 1 ;
            var pageSize = int.Parse(Request["rows"]) != 0 ? int.Parse(Request["rows"]) : 1;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("title", Request["title"]);
            dic.Add("receiver", Request["receiver"]);
            
            PageList<NoticEntity> pageList = DALUtility.MDbS.PageList<NoticEntity>("XMShop", "notic",
                x => 1==1,
                null, pageIndex, pageSize, null, true);
            

           List<NoticEntity> nes = pageList.Items;
           

            return PagerData(pageList.Total, pageList.Items, pageList.PageIndex, pageList.PageSize);
        }

        /// <summary>
        /// 功能:返回添加公告的模态框
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
        {
            DateTime dt = DateTime.Now;
            string StartWeek = dt.ToString("yyyy-MM-dd"); //获取一周的开始日期
            string EndWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).AddDays(6).ToString("yyyy-MM-dd"); //获取本周星期天日期

            ViewData["StartWeek"] = StartWeek;
            ViewData["EndWeek"] = EndWeek;
            return View("_Form");
        }

        /// <summary>
        /// 功能:删除一个公告
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string id = Request["id"];
            long iCheck = DALUtility.MDbS.Delete<NoticEntity>("XMShop", "notic", x => x._id == id);
            if (iCheck == 0)
            {
                return OperationReturn(false, "删除公告失败!");
            }
            return OperationReturn(true, "删除公告成功!");
        }


    }
}