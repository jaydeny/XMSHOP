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
        /// <summary>
        /// 功能:返回充值,反充值页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RechargePage()
        {
            setMark();
            ViewData["Remainder"] = Remainder;
            ViewData["Integral"] = Integral;
            return View("_Recharge");
        }

        /// <summary>
        /// 功能:充值游戏积分
        /// </summary>
        /// <returns></returns>
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
                    return OperationReturn(false, "game003");
                }
            }

            string code = Guid.NewGuid().ToString();

            string[] paras = { Agent_Acc.ToString(), AN, Money,code };

            string strKey = Md5.GetMd5(paras[0] + paras[1] + paras[2] + paras[3] + GameUtil.KEY);

            string param = GameReturn("EditCredit", strKey, paras);

            var result = GameUtil.HttpPost(param);

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
                    return OperationReturn(true, iCheck == 1 ? "game004" : "game005");
                }
            }
            return OperationReturn(boo, str);
        }

        /// <summary>
        /// 功能:确认充值师傅成功
        /// </summary>
        /// <param name="AN"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CheckRecharge(string AN,string code)
        {
            string[] paras = { code,AN };

            string strKey = Md5.GetMd5(paras[0] + paras[1] + GameUtil.KEY);

            string param = GameReturnS("EditCreditConfirm", strKey, paras);

            var result = GameUtil.HttpPost(param);
            return result;
        }

        /// <summary>
        /// 功能:刷新积分
        /// </summary>
        public void setMark()
        {
            string[] paras = { AN };

            string strKey = Md5.GetMd5(paras[0] + GameUtil.KEY);

            string param = GameReturn("GetCredit", strKey, paras);

            var result = GameUtil.HttpPost(param);


            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("vip_AN", AN);
            decimal remainder = DALUtility.Vip.QryRemainder(dic);

            int x = result.LastIndexOf(":");
            string y = result.Substring(x);
            int z = y.IndexOf("}");
            Integral = y.Substring(1, z - 1) == "[]" ? "0" : y.Substring(1, z - 1);
            Session["Remainder"] = remainder.ToString();
        }
    }
}