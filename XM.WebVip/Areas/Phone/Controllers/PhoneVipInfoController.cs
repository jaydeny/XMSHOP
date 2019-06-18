using System.Web.Mvc;
using XM.WebVip.Controllers;
/// <summary>
/// 作者:梁钧淋
/// 日期:2019/5/28
/// </summary>
namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 用户个人中心
    /// </summary>
    public class PhoneVipInfoController : VipInfoController
    {
        /// <summary>
        /// 返回个人中心页
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoPage_MB()
        {
            return View();
        }
        /// <summary>
        /// 获取用户名字
        /// </summary>
        /// <returns></returns>
        public ActionResult getName() {
            if (Session["AN"] == null)
                return OperationReturn(false, "", "请登录...");
            return OperationReturn(true, "", Session["AN"]);
        }
    }
}