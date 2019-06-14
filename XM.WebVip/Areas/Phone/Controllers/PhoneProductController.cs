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
        /// <summary>
        /// 获取品牌商品
        /// </summary>
        /// <returns></returns>
        public ActionResult getBrand() {
           return this.BoutiqueGoods();
        }
        /// <summary>
        /// 获取热门商品
        /// </summary>
        /// <returns></returns>
        public ActionResult getPopular()
        {
            return this.HotGoods();
        }
        /// <summary>
        /// 获取商品详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult getGoodsInfo()
        {
            var id = Request["id"];
            if (id == "") {
                return OperationReturn(false, "数据传输错误");
            }

            var res = DALUtility.Agent.QryAgoodsByID(Convert.ToInt32(id));
            if (res == null || res == "") {
                return OperationReturn(false, "查询无数据");
            }

            return Content(res);
        }

        /// <summary>
        /// 返回商品购物页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductInfo()
        {
            return View();
        }
    }
}