using System.Web.Mvc;
using XM.WebVip.Controllers;
/// <summary>
/// 作者:梁钧淋
/// 日期:2019/5/28
/// </summary>
namespace XM.WebVip.Areas.Phone.Controllers
{
    /// <summary>
    /// 功能:会员端手机版操作,登录等方法
    /// </summary>
    /// <returns>页面:首页</returns>
    public class PhoneHomeController : HomeController
    {

        public ActionResult Index_MB()
        {
            return View();
        }

        /// <summary>
        /// 手机端登陆方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignIn_MB()
        {
            string AN = Request["name"];
            string pwd = Request["password"];
            return Login(AN, pwd);
        }
        /// <summary>
        /// 返回登陆页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login_MBV()
        {
            return View();
        }
        /// <summary>
        /// 用户退出登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUp_MB()
        {
            return RemoveSession();
        }
    }
}