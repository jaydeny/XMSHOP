/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{

    /// <summary>
    /// 功能：代理端信息,跳转代理端个人信息页等方法
    /// </summary>
    public class AgentInfoController : BaseController
    {
        #region _AgentInfo
        /// <summary>
        /// 功能:返回代理个人信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AgentPersonalCenter()
        {
            return View();
        }
        /// <summary>
        /// 获取代理个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult QryAgentInfo()
        {
            if (Session["Agent_AN"] != null)
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras.Add("agent_AN", Session["Agent_AN"].ToString());

                return Content(DALUtility.Agent.QryAgentInfo<AgentEntity>(paras));
            }
            else
            {
                return OperationReturn(false, "请登录后查看个人信息");
            }
        }
        #endregion

    }
}