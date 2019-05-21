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

            return PagerData(pageList.Total, pageList.Items, pageList.PageIndex, pageList.PageSize);
        }

        public ActionResult Delete()
        {
            string id = Request["id"];
            long iCheck = DALUtility.MDbS.Delete<NoticEntity>("XMShop", "notic", x => x._id == id);
            if (iCheck > 0)
            {
                return OperationReturn(false, "删除公告失败!");
            }
            return OperationReturn(true, "删除公告成功!");
        }
        
        public static Expression AddPredicate(Dictionary<string,object> dic)
        {

            foreach (var entry in dic)
            {
                if (entry.Value.Equals(""))
                {

                }
            }


            Expression con = Expression.Call
            (
                typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
                Expression.Constant("a")
            );
            return con;
        }
    }
}