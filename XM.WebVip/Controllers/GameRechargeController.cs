using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Comm;
using XM.Web.Controllers;

namespace XM.WebVip.Controllers
{
    public class GameRechargeController : BaseController
    {
        // GET: GameRecharge
        public ActionResult RechargePage()
        {
            return View("_Recharge");
        }

        [HttpPost]
        public ActionResult Recharge()
        {
            string Money = Request["money"];
            string Code = Request["code"];

            if (Code.Equals("2"))
            {
                Money = "-" + Money;
            }

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("vip_AN", AN);
            decimal remainder = DALUtility.Xm.CheckRamainder(dic);
            if(remainder < decimal.Parse(Money))
            {
                return OperationReturn(false,"余额不足,请充值后再试!");
            }

            string code = Guid.NewGuid().ToString();

            string[] paras = { base.Agent_AN, AN, Money,code };

            string strKey = Md5.GetMd5(paras[0] + paras[1] + paras[2] + paras[3] + KEY);

            string param = GameReturn("EditCredit", strKey, paras);

            var result = Post("http://172.16.31.232:9678/take", param);

            bool boo = false;
            string str = "充值失败";
            if(!result.Contains("1"))
            {
                var resultConfirm = CheckRecharge(AN,code);

                if (!result.Contains("1"))
                {
                    dic.Add("money", Request["money"]);
                    if (Code.Equals("1"))
                    {
                        if (DALUtility.Xm.GameRecharge(dic) > 0)
                        {
                            boo = true;
                            str = "充值成功";
                        }
                    }
                    else
                    {
                        if (DALUtility.Xm.ShopRecharge(dic) > 0)
                        {
                            boo = true;
                            str = "反充值成功";
                        }
                    }
                }
            }
            return OperationReturn(boo, str);
        }

        public static string CheckRecharge(string AN,string code)
        {
            string[] paras = { AN,  code };

            string strKey = Md5.GetMd5(paras[0] + paras[1] + KEY);

            string param = GameReturnS("EditCreditConfirm", strKey, paras);

            var result = Post("http://172.16.31.232:9678/take", param);
            return result;
        }
    }
}