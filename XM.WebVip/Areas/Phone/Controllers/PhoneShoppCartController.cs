using System.Web.Mvc;
using XM.WebVip.Controllers;
/// <summary>
/// 作者:梁钧淋
/// 日期:2019/5/28
/// </summary>
namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class PhoneShoppCartController : ShoppCartController
    {
        /// <summary>
        /// 返回购物车页
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCart_MB()
        {
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
        /// <summary>
        /// 批量删除购物车项
        /// </summary>
        /// <param name="items">购物车项id数组</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleCarts_MB(int[] items)
        {
            return deleCarts(items);
        }

    }
}