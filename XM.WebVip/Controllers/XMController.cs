using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Web.Controllers;

namespace XM.WebVip.Controllers
{
    public class XMController : BaseController
    {
        // GET: XM
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCredit()
        {
            return View();
        }

        public ActionResult EditCreditPage()
        {
            ViewData.Model = System.Guid.NewGuid().ToString();
            return View();
        }

        [HttpPost]
        public ActionResult EditCredit()
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("vip_AN", Request["vip_AN"]);
            decimal result = DALUtility.Xm.CheckRamainder(pairs);

            if(result >= decimal.Parse(Request["Money"]))
            {
                return OperationReturn(true, "余额充足");
            }
            return OperationReturn(false, "余额不足");
        }
    }
}