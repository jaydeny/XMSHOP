/*-------------------------------------*
 * 创建人:         梁钧淋
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       梁钧淋       创建
 *-------------------------------------*/
using System.Web.Mvc;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 用户个人中心
    /// </summary>
    public class PhoneVipInfoController : VipInfoController
    {
        #region view
        /// <summary>
        /// 返回个人中心页
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoPage_MB()
        {
            return View();
        }
        #endregion

        #region select
        /// <summary>
        /// 获取用户名字
        /// </summary>
        /// <returns></returns>
        public ActionResult getName() {
            if (Session["AN"] == null)
                return OperationReturn(false, "", "请登录...");
            return OperationReturn(true, "", Session["AN"]);
        }
        #endregion
    }
}