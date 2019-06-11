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
        /// <summary>
        /// 功能:进入购物车页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCartPage()
        {
            //是否有用户登录
            ViewData["VipAccountName"] = Session["AN"];
            return View();
        }

        /// <summary>
        /// 功能:商品详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult AgoodsDetail()
        {
            int id = int.Parse(Request["id"].ToString());
            ViewData["ac"] = "";

            AgoodsDTO Agoods = DALUtility.Vip.QryAgoodsDetail(id);

            return View(Agoods);
        }
         
        public ActionResult ChooseAddress()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_id", ID);
            List<AddressEntity> AddList = DALUtility.Vip.QryAllAdd(param);
            ViewData["AddList"] = AddList;
            return View();
        }

        
    }
}