using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Comm;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    public class GameRechargeController : BaseController
    {
        // GET: GameRecharge
        public ActionResult RechargePage()
        {
            setMark();
            ViewData["Remainder"] = Remainder;
            ViewData["Integral"] = Integral;
            return View("_Recharge");
        }

        [HttpPost]
        public ActionResult Recharge()
        {
            string Money = Request["money"];
            string Code = Request["code"];
            string Name = "充值游戏积分";
            DateTime date = DateTime.Now;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("vip_AN", AN);

            if (Code.Equals("2"))
            {
                Money = "-" + Money;
                Name = "提取游戏积分";
            }
            else
            {
                decimal remainder = DALUtility.Xm.CheckRamainder(dic);
                if (remainder < decimal.Parse(Request["money"]))
                {
                    return OperationReturn(false, "余额不足,请充值后再试!");
                }
            }

            string code = Guid.NewGuid().ToString();

            string[] paras = { Agent_Acc.ToString(), AN, Money,code };

            string strKey = Md5.GetMd5(paras[0] + paras[1] + paras[2] + paras[3] + KEY);

            string param = GameReturn("EditCredit", strKey, paras);

            var result = HttpPost("http://172.16.31.249:9678/take", param);

            bool boo = false;
            string str = "充值失败";
            if(!result.Contains("1"))
            {
                if (!CheckRecharge(AN, code).Contains("1"))
                {
                    dic.Add("code", Code);
                    dic.Add("recharge_name", Name);
                    dic.Add("recharge_integral", Request["money"]);
                    dic.Add("recharge_time", date);
                    dic.Add("agent_AN", Agent_Acc);
                    int iCheck = DALUtility.Xm.GameRecharge(dic);
                    return OperationReturn(true, iCheck == 1 ? "充值成功" : "提现成功");
                }
            }
            return OperationReturn(boo, str);
        }

        public static string CheckRecharge(string AN,string code)
        {
            string[] paras = { code,AN };

            string strKey = Md5.GetMd5(paras[0] + paras[1] + KEY);

            string param = GameReturnS("EditCreditConfirm", strKey, paras);

            var result = HttpPost("http://172.16.31.232:9678/take", param);
            return result;
        }
    }
}