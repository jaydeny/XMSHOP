using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using XM.Comm;
using XM.Web.Controllers;

namespace XM.WebVip.Controllers
{
    public class GameHomeController : BaseController
    {
        // GET: GameHome
        public ActionResult Login()
        {
            if (Session["AN"] != null)
            {
                
                string[] paras = { Agent_Acc, AN, "0" };

                string strKey = Md5.GetMd5(paras[0] + paras[1] + paras[2] + KEY);
                //string strKey = "1ad74a37af2a9894e1fed6ed542020df";

                string param = GameReturn("Login", strKey, paras);

                var result = HttpPost("http://172.16.31.232:9678/take", param);
                return OperationReturn(true, result);
            }
            return OperationReturn(false, "请登录后进入游戏!");
        }

        [HttpPost]
        public ActionResult GetCredit()
        {
            if (Session["AN"] != null)
            {
                string[] paras = { AN };

                string strKey = Md5.GetMd5(paras[0] + KEY);

                string param = GameReturn("GetCredit", strKey, paras);

                var result = HttpPost("http://172.16.31.232:9678/take", param);

                int x = result.LastIndexOf(":");
                string y = result.Substring(x);
                int z = y.IndexOf("}");
                string p = y.Substring(1,z-1);
                return OperationReturn(true, result);
            }
            return OperationReturn(false, "请登录后查询积分!");
        }
    }


}