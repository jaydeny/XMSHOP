/*-------------------------------------*
 * 创建人:         梁钧淋
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       梁钧淋       创建
 *-------------------------------------*/
using System.Web.Mvc;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class PhoneShoppCartController : ShoppCartController
    {
        #region view
        /// <summary>
        /// 返回购物车页
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCart_MB()
        {
            return View();
        }
        #endregion

        #region select
        /// <summary>
        /// 获取购物车信息方法
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCartByVIPID_MB()
        {
            return GetCartByVIPID();
        }
        #endregion

        #region edit
        /// <summary>
        /// 编辑购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult EditCart_MB()
        {
            return EditCart();
        }
        #endregion

        #region delete
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
        #endregion
    }
}