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
    /// 功能：代理商的登录,注册,修改,安全退出等方法
    /// </summary>
    public class HomeController : BaseController
    {
        // GET: Home
        #region _Login
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:返回代理端登录页
        /// </summary>
        /// <returns>页面:代理端登录页</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 作者:梁钧淋
        /// 日期:2019/5/5
        /// 功能:返回代理端主页
        /// </summary>
        /// <returns>页面:代理端主页</returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行登入
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult Login(string AN, string pwd)
        {
            try
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras["agent_AN"] = AN;
                paras["agent_pwd"] = pwd;

                var agent = DALUtility.Agent.QryAgentToLogin<AgentEntity>(paras);
                if (agent != null)
                {
                    if (agent.StatusID == 2)
                    {
                        return OperationReturn(false, "用户已被禁用，请您联系管理员");
                    }
                    
                    Session["Agent_AN"] = agent.AgentAccountName;
                    Session["Agent_ID"] = agent.AgentID;
                    SSOAgent.Add(agent,"onLine");
                    return OperationReturn(true, "登录成功,agent_id:" + agent.AgentID + ";agent_AN:" + AN,
                        new
                        {
                            agent_id = agent.AgentID,
                            agent_AN = agent.AgentAccountName,
                        });
                }
                else
                {
                    return OperationReturn(false, "用户名密码错误，请您检查");
                }
            }
            catch (Exception ex)
            {
                return OperationReturn(false, "登录异常," + ex.Message);
            }
        }
        #endregion

        #region _Signin
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:返回代理端注册页
        /// </summary>
        /// <returns>页面:代理端注册页</returns>
        public ActionResult Signin()
        {
            return View();
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行注册
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult Signin(AgentEntity agent)
        {
            return Save(0);
        }
        #endregion

        #region _Update
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行修改信息
        /// </summary>
        /// <returns>页面:修改页面</returns>
        public ActionResult Update()
        {
            return View();
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行修改信息
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult Update(AgentEntity agent)
        {
            return Save(int.Parse(Session["Agent_ID"].ToString()));
        }

        #endregion
        
        #region _自定义

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4-28
        /// 修改时间：2019-
        /// 功能：安全退出
        /// </summary>
        /// 
        public ActionResult RemoveSession()
        {
            Session.Remove("Agent_ID");
            Session.Remove("Agent_AN");
            return OperationReturn(true, "退出成功!");
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/25
        /// 功能:添加/修改代理的公用方法
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        //注册或者修改代理信息时,检查邮箱,email,联系方式舒服重复
        public ActionResult Save(int ID)
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = ID;
            paras["agent_AN"] = Session["Agent_AN"] != null ? Session["Agent_AN"].ToString() : Request["Agent_AN"];
            paras["agent_mp"] = Request["agent_mp"];
            paras["agent_email"] = Request["agent_email"];

            int iCheck = DALUtility.Agent.CheckANandMBandEmail(paras);

            if (iCheck > 0)
            {
                return OperationReturn(false, iCheck == 1 ? "所输入的用户名重复,请重新输入!" : (iCheck == 2 ? "所输入的手机号码重复,请重新输入!" : "所输入的邮箱重复,请重新输入!"));
            }
            else
            {
                paras["agent_pwd"] = Request["agent_pwd"];
                paras["agent_CBY"] = Request["agent_CBY"];
                paras["agent_CDT"] = DateTime.Now;
                paras["status_id"] = 1;
                int result = DALUtility.Agent.SaveAgent(paras);
                if (ID == 0)
                {

                    return OperationReturn(true, "注册成功");
                }
                else
                {
                    return OperationReturn(true, "修改成功");
                }
            }
        }
        #endregion
    }
}