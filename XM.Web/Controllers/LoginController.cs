using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mail;
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
        /// <param name="CookieExpires">cookie有效期</param>
        /// <returns></returns>
        public ActionResult CheckUserLogin(string CookieExpires)
        {
            try
            {
                var iUserDal = DALUtility.User;
                var currentUser = iUserDal.UserLogin(Request["user_AN"], Request["user_pwd"]);
                if (currentUser != null)
                {
                    //记录登录cookie
                    CookiesHelper.SetCookie("UserID", AES.EncryptStr(currentUser.id.ToString()));
                    Dictionary<string, object> paras= new Dictionary<string, object>();
                    paras["r_id"] = currentUser.RoleID;
                    int iCount;
                    var roleMenus  = DALUtility.RoleMenu.QryAllRoleMenu<RoleMenuEntity>(paras,out iCount);
                    Session["RoleMenu"] = roleMenus;
                    Session["RoleID"] = currentUser.RoleID;
                    Session["User"] = currentUser;
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
        public ActionResult ForgetPwd()
        {
            return View();
        }
        public ActionResult PwdForget()
        {
            bool f = false;
            string user = Request["user_AN"];
            var iUserDal = DALUtility.User;
            var currentUser = iUserDal.GetUserByAccountName(user);
            Session["User"] = currentUser;
            //链接地址必须是绝对地址
            string mailContent = "<a href='http://172.16.31.234:6666/User/PwdUpdate'>修改密码</a>";
            if (currentUser != null)
            {
                f = EmailHelper.send(currentUser.UserEmail, "点击链接修改密码", mailContent);
            }
            return OperationReturn(f,"邮件已发送！");
        }
        public ActionResult UserLoginOut()
        {
            //清空cookie
            CookiesHelper.AddCookie("UserID", System.DateTime.Now.AddDays(-1));
            return OperationReturn(true,"退出成功！");
        }
    }
}