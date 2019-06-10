using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Comm;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    public class ShoppingCartController : BaseController
    {
        // GET: ShoppingCart
        public ActionResult AgoodsDetail()
        {
            int id = int.Parse(Request["id"].ToString());

            AgoodsDTO Agoods = DALUtility.Vip.QryAgoodsDetail(id);
            
            return View(Agoods);
        }


        public ActionResult ShoppingCartPage()
        {
            ViewData["VipAccountName"] = Session["AN"];
            return View();
        }
    }
}