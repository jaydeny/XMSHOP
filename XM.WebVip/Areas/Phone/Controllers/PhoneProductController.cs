using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    public class PhoneProductController : ProductController
    {
        // GET: Phone/PhoneProduct
        public ActionResult AgoodsList_MB()
        {
            return View();
        }
        public ActionResult Search_MB()
        {
            return View();
        }


    }
}