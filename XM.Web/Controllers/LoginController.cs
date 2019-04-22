using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Comm;
using XM.DALFactory;
using XM.Model;

namespace XM.Web.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 处理登录的信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="CookieExpires">cookie有效期</param>
        /// <returns></returns>
        public ActionResult CheckUserLogin(UserEntity userInfo, string CookieExpires)
        {
            try
            {
                var iUserDal = DALUtility.User;
                var currentUser = iUserDal.UserLogin(userInfo.UserAccountName, userInfo.UserPassword);
                if (currentUser != null)
                {
                    //记录登录cookie
                    CookiesHelper.SetCookie("UserID", AES.EncryptStr(currentUser.UserID.ToString()));

                    return OperationReturn(true, "登录成功！"); 
                }
                else
                {
                    return OperationReturn(false, "登录失败！用户名或者密码错误！");
                }
            }
            catch (Exception ex)
            {
                return OperationReturn(false,"登录异常," + ex.Message);
            }
        }

        public ActionResult UserLoginOut()
        {
            //清空cookie
            CookiesHelper.AddCookie("UserID", System.DateTime.Now.AddDays(-1));
            return OperationReturn(true,"退出成功！");
        }
    }
}