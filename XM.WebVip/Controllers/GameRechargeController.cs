/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Comm;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    /// <summary>
    /// 游戏积分
    /// </summary>
    public class GameRechargeController : BaseController
    {
        #region view
        /// <summary>
        /// 功能:返回充值,反充值页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RechargePage()
        {
            //刷新积分的方法
            setMark();
            //将游戏积分和商城积分放到ViewData里
            ViewData["Remainder"] = Remainder;
            ViewData["Integral"] = Integral;
            return View("_Recharge");
        }
        #endregion

        #region Examine
        /// <summary>
        /// 功能:充值游戏积分
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Recharge()
        {
            //获取需要充值的积分额度
            string Money = Request["money"];
            //标识,1 : 商城to游戏 ; 2 : 游戏to商城
            string Code = Request["code"];
            string Name = "充值游戏积分";
            DateTime date = DateTime.Now;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("vip_AN", AN);

            //游戏to商城的情况下,积分额度又变成负数传入
            if (Code.Equals("2"))
            {
                Money = "-" + Money;
                Name = "提取游戏积分";
            }
            else
            {
                //商城to游戏的情况下,判断商城积分是否足够
                decimal remainder = DALUtility.Xm.CheckRamainder(dic);
                //不够的话,返回相应数据
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
            //判断结果
            if(!result.Contains("1"))
            {
                //CheckRecharge方法是查询充值回执,相当于确认方法
                var resultCon = CheckRecharge(AN, code);
                if (!resultCon.Contains("1"))
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
            return OperationReturn(false, "充值失败");
        }

        /// <summary>
        /// 功能:确认充值是否成功
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
        #endregion

        #region integral
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
        #endregion
    }
}