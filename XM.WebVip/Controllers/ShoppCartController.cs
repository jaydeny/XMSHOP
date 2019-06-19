/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShoppCartController : BaseController
    {
        #region view
        /// <summary>
        /// 返回购物车页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据用户ID获取对应的购物车项
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCartPage()
        {
            if (Session["id"] == null)
            {
                List<ShoppCartEntity> list = new List<ShoppCartEntity>();
                var result = cartTable.Values;
                foreach (ShoppCartEntity item in result)
                {
                    list.Add(item);
                }
                ViewData["list"] = list;
                return View();
            }

            var data = DALUtility.ShoppCart.QryDataByVIPID(Convert.ToInt32(ID));
            ViewData["VipAccountName"] = Session["AN"];
            ViewData["Add"] = GetAllAdd(Session["ID"].ToString());
            ViewData["Ac"] = GetAllAc(Session["Agent_Acc"].ToString());
            ViewData["list"] = data;
            return View();
        }
        #endregion

        #region ShoppCart
        /// <summary>
        /// 根据用户ID获取对应的购物车项
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCartByVIPID()
        {
            if (Session["id"] == null)
            {
                return OperationReturn(true, "未登录状态", cartTable.Values);
            }

            var data = DALUtility.ShoppCart.QryDataByVIPID(Convert.ToInt32(ID));
            return OperationReturn(true, "登录状态", data);
        }
        /// <summary>
        /// 编辑购物车
        /// editType 操作类型 ， 1，添加/修改  2，删除
        /// itemID 购物项ID 删除使用
        /// vipID 用户ID
        /// AgoodsID 商品ID
        /// count 数量
        /// acID 活动ID
        /// </summary>
        /// <returns></returns>
        public ActionResult EditCart()
        {
            string editType = Request["editType"];
            if (editType == null)
                return OperationReturn(false, "数据传输错误");
            string itemID = Request["itemID"];
           
            int AgoodsID = Convert.ToInt32(Request["AgoodsID"]);
            int count = Convert.ToInt32(Request["count"]);

            if (Session["id"] == null)
            {
                ShoppCartEntity cartEntity = new ShoppCartEntity();
                cartEntity.Agoods_ID = AgoodsID;
                cartEntity.Agoods_Count = count;
                GoodsEntity goods = DALUtility.Goods.QryGoodsInfo(AgoodsID.ToString());
                cartEntity.GoodsPrice = goods.GoodsPrice;
                cartEntity.GoodsName = goods.GoodsName;
                cartEntity.GoodsPicture = goods.GoodsPicture;
                cartEntity.GoodsType = goods.GoodsType;
                cartEntity.GoodsIntro = goods.GoodsIntro;
                if (cartTable.ContainsKey(AgoodsID))
                    cartTable.Remove(AgoodsID);
                cartTable.Add(AgoodsID, cartEntity);
                return OperationReturn(true, "添加到购物车成功");
            }

            string vipID = ID;
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["editType"] = editType;
            paras["itemID"] = itemID;
            paras["vipID"] = vipID;
            paras["AgoodsID"] = AgoodsID;
            paras["count"] = count;
           
            int res = DALUtility.ShoppCart.EditCart(paras);
            if (res == 2 || res == 4)
                return OperationReturn(false, "操作失败");
            return OperationReturn(true, "操作成功");
        }
        /// <summary>
        /// 批量删除购物车项
        /// </summary>
        /// <param name="items">购物车项ID数组</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleCarts(int[] items) {
            if(Session["id"] == null)
            {
                return OperationReturn(false, "请先登录");
            }
            int len = items.Count();
            foreach (int item in items)
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras["editType"] = 2;
                paras["itemID"] = item;
                paras["vipID"] = ID;
                paras["AgoodsID"] = 0;
                paras["count"] = 0;

                int res = DALUtility.ShoppCart.EditCart(paras);
            }
            return OperationReturn(true, "操作成功");
        }
        #endregion

        #region address
        /// <summary>
        /// 功能:获取会员的地址
        /// </summary>
        /// <returns></returns>
        public List<AddressEntity> GetAllAdd(string vip_id)
        {
            Dictionary<string, object> AddDic = new Dictionary<string, object>();
            AddDic.Add("vip_id", vip_id);

            return DALUtility.Vip.QryAllAdd(AddDic);
        }
        #endregion

        #region Actity
        /// <summary>
        /// 功能:获取符合条件的活动
        /// </summary>
        /// <returns></returns>
        public List<ActivityEntity> GetAllAc(string Agent_AN)
        {
            Dictionary<string, object> AcDic = new Dictionary<string, object>();
            AcDic.Add("agent_AN", Agent_AN);
            AcDic.Add("Date", DateTime.Now);
            return DALUtility.Activity.QryAC<ActivityEntity>(AcDic);
        }
        #endregion
    }
}