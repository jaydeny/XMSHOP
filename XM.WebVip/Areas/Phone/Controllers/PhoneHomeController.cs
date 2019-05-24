using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.WebVip.Controllers;

namespace XM.WebVip.Areas.Phone.Controllers
{
    // GET: Home
    /// <summary>
    /// 作者:梁钧淋
    /// 日期:2019/5/16
    /// 功能:会员端手机版操作,登录等方法
    /// </summary>
    /// <returns>页面:首页</returns>
    public class PhoneHomeController : HomeController
    {

        public ActionResult Index_MB()
        {
            return View();
        }

        //手机端登录方法
        [HttpPost]
        public ActionResult Login_MB()
        {
            string AN = Request["name"];
            string pwd = Request["password"];
            return Login(AN,pwd);
        }

    }
}