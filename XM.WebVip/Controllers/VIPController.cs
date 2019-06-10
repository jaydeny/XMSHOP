using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XM.WebVip.Controllers
{
    public class VIPController : Controller
    {
        // GET: VIP
        public ActionResult Index()
        {
            return View("GoodsDetails");
        }
    }
}