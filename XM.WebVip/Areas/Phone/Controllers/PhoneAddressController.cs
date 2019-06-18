using System.Collections.Generic;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;
/// <summary>
/// 作者:梁钧淋
/// 日期:2019/5/28
/// </summary>
namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 地址
    /// </summary>
    public class PhoneAddressController : BaseController
    {
        /// <summary>
        /// 返回地址页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取用户所有地址
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