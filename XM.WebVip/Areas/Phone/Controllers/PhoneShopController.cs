﻿/*-------------------------------------*
 * 创建人:         梁钧淋
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       梁钧淋       创建
 *-------------------------------------*/
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Model;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 商品购买
    /// </summary>
    public class PhoneShopController : ShopController
    {
        #region select
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
        #endregion

        #region buy
        /// <summary>
        /// 批量购买
        /// </summary>
        /// <param name="buys"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PhoneBuyToPro(List<BuyStructEntity> buys) {

            return BuyToPro(buys);
        }
        #endregion
    }
}