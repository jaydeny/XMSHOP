using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/// <summary>
/// 作者:曾贤鑫
/// 日期:2019/5/13
/// </summary>
namespace XM.WebVip.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class VIPController : Controller
    {
        /// <summary>
        /// 返回用户商品详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("GoodsDetails");
        }
    }
}