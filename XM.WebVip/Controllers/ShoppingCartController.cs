/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    /// <summary>
    /// pc端购物车
    /// </summary>
    public class ShoppingCartController : BaseController
    {
        #region view
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
         
        /// <summary>
        /// 功能:选择地址
        /// </summary>
        /// <returns></returns>
        public ActionResult ChooseAddress()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_id", ID);
            List<AddressEntity> AddList = DALUtility.Vip.QryAllAdd(param);
            ViewData["AddList"] = AddList;
            return View();
        }
        #endregion

    }
}