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
        /// <param name="userInfo"></param>
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
            bool f = false;
            string user = Request["user_AN"];
            var iUserDal = DALUtility.User;
            var currrentUser = iUserDal.GetUserByAccountName(user);
            //string code = RandCode(8);
            //链接地址必须是绝对地址
            string mailContent = "<a href='https://fanyi.baidu.com/?aldtype=16047#zh/en/'>百度</a>";
            if (currrentUser != null)
            {
                f = EmailHelper.send(currrentUser.UserEmail, "验证码", mailContent);
            }

            return OperationReturn(f,"邮件已发送！");
        }
        public ActionResult UserLoginOut()
        {
            //清空cookie
            CookiesHelper.AddCookie("UserID", System.DateTime.Now.AddDays(-1));
            return OperationReturn(true,"退出成功！");
        }



        public static string RandCode(int n)
        {
            char[] arrChar = new char[]{
           'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',
           '0','1','2','3','4','5','6','7','8','9',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
          };

            StringBuilder num = new StringBuilder();

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < n; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());

            }

            return num.ToString();
        }
    }
}