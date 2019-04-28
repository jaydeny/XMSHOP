using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;

namespace XM.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            UserEntity uInfo = Session["Account"] as UserEntity;
            if (uInfo == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.RealName = uInfo.UserAccountName;
            ViewBag.TimeView = DateTime.Now.ToLongDateString();
            ViewBag.DayDate = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("m_id",typeof(int));
            dataTable.Columns.Add("m_id", typeof(bool));
            //dataTable.Rows.Add(new DataRow() { })
            return View();
        }
    }
}