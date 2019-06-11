using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
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
            int id;
            try
            {
                id = Convert.ToInt32(Request["id"]);
            }
            catch (Exception)
            {
                return OperationReturn(false, "数据输入错误，请重新输入");
            }

            var data = DALUtility.ShoppCart.QryDataByVIPID(id);
            return OperationReturn(true, "操作成功", data);
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
            string vipID = Request["vipID"];
            string AgoodsID = Request["AgoodsID"];
            string count = Request["count"];
            string acID = Request["acID"];

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["editType"] = editType;
            paras["itemID"] = itemID;
            paras["vipID"] = vipID;
            paras["AgoodsID"] = AgoodsID;
            paras["count"] = count;
            paras["acID"] = acID;

            int res = DALUtility.ShoppCart.EditCart(paras);
            if (res == 2 || res == 4)
                return OperationReturn(false, "操作失败");
            return OperationReturn(true, "操作成功");
        }



    }
}