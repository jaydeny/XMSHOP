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
    /// 功能:会员端手机版操作,登录等方法
    /// </summary>
    /// <returns>页面:首页</returns>
    public class PhoneHomeController : HomeController
    {
        #region view
        public ActionResult Index_MB()
        {
            return View();
        }
        /// <summary>
        /// 返回登陆页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login_MBV()
        {
            return View();
        }
        #endregion

        #region SignIn
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
        #endregion

        #region SignUp
        /// <summary>
        /// 用户退出登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUp_MB()
        {
            return RemoveSession();
        }
        #endregion
    }
}