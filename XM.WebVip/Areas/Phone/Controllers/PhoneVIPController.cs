using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    public class PhoneVIPController : VIPController
    {
        // GET: Phone/PhoneVIP
        public ActionResult ShoppingCart_MB()
        {
            return View();
        }
    }
}