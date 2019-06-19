/*-------------------------------------*
 * 创建人:         梁钧淋
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       梁钧淋       创建
 *-------------------------------------*/
using System;
using System.Web.Mvc;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 商品
    /// </summary>
    public class PhoneProductController : ProductController
    {
        #region view
        /// <summary>
        /// 返回商品列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AgoodsList_MB()
        {
            return View();
        }
        /// <summary>
        /// 返回搜索页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Search_MB()
        {
            return View();
        }
        /// <summary>
        /// 返回商品购物页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductInfo()
        {
            return View();
        }
        #endregion

        #region product
        /// <summary>
        /// 获取品牌商品
        /// </summary>
        /// <returns></returns>
        public ActionResult getBrand() {
           return BoutiqueGoods();
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
        #endregion
    }
}