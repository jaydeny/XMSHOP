using System.Web.Mvc;
using XM.Comm;
using XM.WebVIP.Controllers;
/// <summary>
/// 作者:曾贤鑫
/// 日期:2019/5/13
/// </summary>
namespace XM.WebVip.Controllers
{
    /// <summary>
    /// 游戏
    /// </summary>
    public class GameHomeController : BaseController
    {
      
        /// <summary>
        /// 功能:登录到游戏
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //判断是否有会员在线
            if (Session["AN"] != null)
            {
                //传入游戏后台的数据
                string[] paras = { Agent_Acc, AN, "0" };
                //md5 加密出key
                string strKey = Md5.GetMd5(paras[0] + paras[1] + paras[2] + GameUtil.KEY);

                string param = GameReturn("Login", strKey, paras);
                //结果
                var result = GameUtil.HttpPost(param);
                return OperationReturn(true, result);
            }
            return OperationReturn(false, "game001");
        }

        /// <summary>
        /// 获取积分
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCredit()
        {
            if (Session["AN"] != null)
            {
                string[] paras = { AN };

                string strKey = Md5.GetMd5(paras[0] + GameUtil.KEY);

                string param = GameReturn("GetCredit", strKey, paras);

                var result = GameUtil.HttpPost(param);

                //截取出当前登录会员的积分
                int x = result.LastIndexOf(":");
                string y = result.Substring(x);
                int z = y.IndexOf("}");
                string p = y.Substring(1,z-1);

                return OperationReturn(true, result);
            }
            return OperationReturn(false, "game002");
        }
    }


}