/*-------------------------------------*
 * 创建人:         梁钧淋
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       梁钧淋       创建
 *-------------------------------------*/
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 地址
    /// </summary>
    public class PhoneAddressController : BaseController
    {
        #region view
        /// <summary>
        /// 返回地址页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region address
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
        #endregion
    }
}