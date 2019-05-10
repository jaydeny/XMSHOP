using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-5/5
    /// 修改时间：2019-
    /// 功能：代理端信息,跳转代理端个人信息页等方法
    /// </summary>
    public class AgentInfoController : BaseController
    {
        // GET: AgentInfo

        #region _AgentInfo
        /// <summary>
        /// 作者:梁钧淋
        /// 日期:2019/4/30
        /// 功能:返回代理个人信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AgentPersonalCenter()
        {
            return View();
        }

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