using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    public class PhoneVipInfoController : VipInfoController
    {
        // GET: Phone/PhoneVipInfo
        public ActionResult InfoPage_MB()
        {
            return View();
        }
    }
}