using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{
    public class AgentController : BaseController
    {
        // GET: Agent
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

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
                    return OperationReturn(true, "登录成功,agent_id:" + AN + ";agent_AN:" + pwd);
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

        //注册时,返回注册页面
        public ActionResult Signin()
        {
            return View();
        }

        //注册
        [HttpPost]
        public ActionResult Signin(AgentEntity agent)
        {
            return save(0);
        }

        //修改代理
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(AgentEntity agent)
        {
            return save(agent.AgentID);
        }

        //注册或者修改代理信息时,检查邮箱,email,联系方式舒服重复
        public ActionResult save(int ID)
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = ID;
            paras["agent_AN"] = Request["agent_AN"];
            paras["agent_mp"] = Request["agent_mp"];
            paras["agent_email"] = Request["agent_email"];

            int iCheck = DALUtility.Agent.checkANandMBandEmail(paras);

            if (iCheck > 0)
            {
                return OperationReturn(false, iCheck == 1 ? "所输入的用户名重复,请重新输入!" : (iCheck == 2 ? "所输入的手机号码重复,请重新输入!" : "所输入的邮箱重复,请重新输入!"));
            }
            else
            {
                paras["agent_pwd"] = Request["agent_pwd"];
                paras["agent_CBY"] = Request["agent_CBY"];
                paras["agent_CDT"] = DateTime.Now;
                paras["status_id"] = Request["status_id"];
                int result = DALUtility.Agent.saveAgent(paras);
                return OperationReturn(result > 0);
            }
        }

        //public ActionResult GetAllVIP()
        //{

        //    string sort = Request["sort"] == null ? "id" : Request["sort"];
        //    string order = Request["order"] == null ? "asc" : Request["order"];
        //    int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
        //    int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

        //    string agent_AN = Request["agent_AN"];
        //    string agent_mp = Request["agent_mp"];
        //    string agent_email = Request["agent_email"];
        //    string status_id = Request["status_id"];

        //    Dictionary<string, object> param = new Dictionary<string, object>();
        //    param.Add("pi", pageindex);
        //    param.Add("pageSize", pagesize);
        //    param.Add("sort", sort);
        //    param.Add("agent_AN", agent_AN);
        //    param.Add("agent_mp", agent_mp);
        //    param.Add("agent_email", agent_email);
        //    param.Add("status_id", status_id);


        //    string result = DALUtility.Agent.QryAllAgent(param, out int ICount);
        //    return Content(result);
        //}

        public ActionResult GetAllUserInfo()
        {
            string sort = Request["sort"] == null ? "VipID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string userAn = Request["vip_AN"] == null ? "" : Request["vip_AN"];
            string userMp = Request["vip_mp"] == null ? "" : Request["vip_mp"];
            string userEmail = Request["vip_email"] == null ? "" : Request["vip_email"];
            int statusId = Request["status_id"] == null ? 1 : Convert.ToInt32(Request["status_id"]);
            string createDateTime = Request["vip_CDT"] == null ? "" : Request["vip_CDT"];
            int agentId = Request["agent_id"] == null ? 1 : Convert.ToInt32(Request["agent_id"]);
            

            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["vip_AN"] = userAn;
            paras["sort"] = sort;
            paras["order"] = order;
            var users = DALUtility.Vip.QryUsers<VipEntity>(paras, out totalCount);
            if (users != null)
            {
                log(HttpContext.Session["user_AN"].ToString(), "查询所有vip用户信息", "true", "查询成功");
            }
            else
            {
                log(HttpContext.Session["user_AN"].ToString(), "查询所有vip用户信息", "false", "查询失败");
            }
            return PagerData(totalCount, users);
        }

        public ActionResult MakeGoods()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", Request["Agooods_id"]);
            param.Add("goods_id", Request["goods_id"]);
            param.Add("status_id", Request["status_id"]);
            param.Add("price", Request["price"]);
            param.Add("up_time", Request["up_time"]);
            param.Add("Agent_AN", Request["Agent_AN"]);

            int iCheck = DALUtility.Agent.MakeGoods(param);
            return OperationReturn(true, iCheck == 0 ? "上架成功" : (iCheck == 1 ? "修改成功!" : "当前操作失败,请重新尝试!"));
        }

        //代理商处理充值
        public ActionResult CheckRecharge(int vip_id, decimal recharge_price,DateTime recharge_time)
        {
            return OperationReturn(true, "用户:"+vip_id+"于"+recharge_time+"充值:"+recharge_price+"元!充值成功!!");
        }


        public ActionResult QryReportForm()
        {
            string sort = Request["sort"] == null ? "id" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("startTime", Request["startTime"]);
            param.Add("endTime", Request["endTime"]);
            param.Add("agent_AN", Request["agent_AN"]);

            string result = DALUtility.Agent.QryReportForm(param, out int ICount);
            return Content(result);
        }
    }
}