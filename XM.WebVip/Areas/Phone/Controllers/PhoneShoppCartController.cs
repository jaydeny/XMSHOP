﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    public class PhoneShoppCartController : ShoppCartController
    {
        /// <summary>
        /// 返回购物车页
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCart_MB() {
            return View();
        }
        /// <summary>
        /// 获取购物车信息方法
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCartByVIPID_MB()
        {
            return GetCartByVIPID();
        }
        /// <summary>
        /// 编辑购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult EditCart_MB()
        {
            return EditCart();
        }



    }
}