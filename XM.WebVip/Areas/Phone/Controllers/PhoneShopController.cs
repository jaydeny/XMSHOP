using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    public class PhoneShopController : ShopController
    {
        /// <summary>
        /// 获取所有活动
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllAc_MB()
        {
            if (Session["id"] == null)
                return OperationReturn(false, "请先登录");
            return OperationReturn(true, "操作成功", GetAllAc());
        }
        /// <summary>
        /// 通过活动类型，活动ID获取活动详情
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAcEntity()
        {
           
            try
            {
                string acType = Request["acType"];
                if (acType == null || acType == "")
                {
                    return OperationReturn(false, "数据传输错误请重新再试");
                }

                int acID = Convert.ToInt32(Request["acID"]);
                if (acType == "1003")
                {
                    return OperationReturn(true, "操作成功", DALUtility.Activity.QryACTypeInfoDis<CustomDisEntity>(acID));
                }
                else if (acType == "1002")
                {
                    return OperationReturn(true, "操作成功", DALUtility.Activity.QryACTypeInfoFull<CustomDisEntity>(acID));
                }
                else {
                    return OperationReturn(false, "数据传输错误请重新再试");
                }
            }
            catch (Exception)
            {
                return OperationReturn(false, "数据传输错误请重新再试");
            }
           
        }


        [HttpPost]
        public ActionResult PhoneBuyToPro(List<BuyStructEntity> buys) {

            return BuyToPro(buys);
        }

    }
}