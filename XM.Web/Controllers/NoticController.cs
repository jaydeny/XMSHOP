using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XM.Web.Controllers
{
    public class NoticController : Controller
    {
        // GET: Notic
        public ActionResult Index()
        {
            DateTime dt = DateTime.Now;
            string StartWeek = dt.ToString("yyyy-MM-dd"); //获取一周的开始日期
            string EndWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).AddDays(6).ToString("yyyy-MM-dd"); //获取本周星期天日期

            ViewData["StartWeek"] = StartWeek;
            ViewData["EndWeek"] = EndWeek;
            return View();
        }

        public ActionResult GetAllAgent()
        {
            return View();
        }
    }
}