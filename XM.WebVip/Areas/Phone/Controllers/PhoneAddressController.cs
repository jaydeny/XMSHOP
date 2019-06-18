using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    public class PhoneAddressController : BaseController
    {
        // GET: Phone/PhoneAddress
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有地址
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllAddress()
        {
            if (Session["id"] == null)
                return OperationReturn(false, "未登录状态");
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_id", ID);
            List<AddressEntity> AddList = DALUtility.Vip.QryAllAdd(param);
            return OperationReturn(true,"成功",AddList);
        }

    }
}