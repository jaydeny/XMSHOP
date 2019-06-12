using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    public class ShoppCartController : BaseController
    {
        // GET: ShoppCart
        public ActionResult Index()
        {
            return View();
        }

        

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
        /// 根据用户ID获取对应的购物车项
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCartPage()
        {
            if (Session["id"] == null)
            {
                List < ShoppCartEntity > list = new List<ShoppCartEntity>();
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
            ViewData["list"] = data;
            return View();
        }
    }
}