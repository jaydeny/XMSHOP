using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Shop
        #region _shopping
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 查看余额
        /// </summary>
        /// <returns></returns>
        public ActionResult Buy()
        {
            if (Session["AN"] == null)
            {
                return OperationReturn(false, "请点击登录页面进行登录");
            }
            else
            {
                //后续需要修改,有关于选中地址的方式
                if (QryAdd() == 0)
                {
                    return OperationReturn(false, "请添加地址后购物");
                }
                var vipInfo = QryTOPAdd();
                DateTime date = DateTime.Now;

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("order_date", date);
                param.Add("order_address", vipInfo.AddressID);
                param.Add("order_mp", vipInfo.VipMobliePhone);
                param.Add("vip_AN", Session["AN"].ToString());
                param.Add("agent_AN", Session["Agent_Acc"].ToString());
                param.Add("order_total", decimal.Parse(Request["order_total"]));

                param.Add("buy_time", date);
                param.Add("buy_count", int.Parse(Request["buy_count"]));
                param.Add("buy_AN", Session["AN"].ToString());
                param.Add("agoods_id", int.Parse(Request["agoods_id"]));
                param.Add("buy_total", decimal.Parse(Request["buy_total"]));

                int iCheck = DALUtility.Vip.Buy(param);

                if (iCheck > 0)
                {
                    return OperationReturn(false, iCheck == 1 ? "用户余额不足,请充值后从试!" : "购物出错,请重试!");
                }
                return OperationReturn(true, "购物成功");
            }
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4/30
        /// 修改时间：2019-
        /// 功能：查询地址
        /// </summary>
        public int QryAdd()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_AN", Session["AN"].ToString());
            var vipInfo = DALUtility.Vip.QryAdd<int>(param);
            return vipInfo;
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4/30
        /// 修改时间：2019-
        /// 功能：查询地址
        /// </summary>
        public VipInfoDTO QryTOPAdd()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_AN", Session["AN"].ToString());
            var vipInfo = DALUtility.Vip.QryTOPAdd<VipInfoDTO>(param);
            return vipInfo;
        }
        #endregion

        #region _order
        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-
        /// 修改时间：2019-
        /// 功能：查询订单
        /// </summary>
        public ActionResult QryOrder()
        {
            string sort = Request["sort"] == null ? "OrderID" : Request["sort"];
            string order = Request["order"] == null ? "desc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            int iCount;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("order", order);
            param.Add("agent_AN", Session["Agent_Acc"].ToString());
            param.Add("vip_AN", Session["AN"].ToString());
            var objOrder = DALUtility.Order.QryOrder<OrderEntity>(param, out iCount);
            return PagerData(iCount,objOrder);
        }
        #endregion
    }
}