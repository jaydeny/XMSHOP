using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/4/22
    /// 代理商
    /// </summary>
    public class AgentController : BaseController
    {
        #region  获取所有代理页面
        [PermissionFilter]
        // GET: Agent
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region  获取所有代理信息
        [PermissionFilter("Agent", "Index")]
        public ActionResult GetAllUserInfo()
        {
            string sort = Request["order"] == null ? "AgentID" : Request["order"];
            string order = Request["sort"] == null ? "asc" : Request["sort"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string userAn = Request["agent_AN"] == null ? "" : Request["agent_AN"];
            string userMp = Request["agent_mp"] == null ? "" : Request["agent_mp"];
            string userEmail = Request["agent_email"] == null ? "" : Request["agent_email"];
            string statusId = Request["status_id"] == null ? "" : Request["status_id"];



             int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["AgentName"] = userAn;
            paras["sort"] = sort;
            paras["order"] = order;
            var users = DALUtility.Agent.QryUsers<AgentEntity>(paras, out totalCount);
            return PagerData(totalCount, users,pageindex,pagesize);
        }
        #endregion
        #region 添加/修改页面
        public ActionResult Form()
        {
            return View("_Form");
        }
        #endregion
        #region  添加/修改代理信息
        [PermissionFilter("Agent", "Index",Operationype.Add)]
        public ActionResult Save()
        {
            int id = Request["id"] == "" ? 0 : Convert.ToInt32(Request["id"]);
            string userid = Request["AgentAccountName"];
            string mobilephone = Request["MobliePhone"];
            string email = Request["Email"];
            int statusID = Convert.ToInt32(Request["StatusID"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["agent_AN"] = userid;
            paras["agent_mp"] = mobilephone;
            paras["agent_email"] = email;
            paras["status_id"] = statusID;

            int iCheck = DALUtility.Agent.CheckUseridAndEmail(paras);
            ContentResult result = OperationReturn(true);
            if (iCheck > 0)
            {
                switch (iCheck)
                {
                    case 1:
                        result = OperationReturn(false, "用户名重复");
                        break;
                    case 2:
                        result = OperationReturn(false, "电话号码重复");
                        break;
                    case 3:
                        result = OperationReturn(false, "邮箱重复");
                        break;
                }
                return result;
            }
            else
            {
                
                if (id == 0)
                {
                    paras["agent_pwd"] = "xm123456";
                    paras["agent_CBY"] = "admin";
                    paras["agent_CDT"] = DateTime.Now;
                    return OperationReturn(DALUtility.Agent.Save(paras) > 0, "添加成功！初始密码：" + paras["agent_pwd"]);
                }
                return OperationReturn(DALUtility.Agent.Save(paras) > 0, "修改成功！");
            }
            
        }
        #endregion
        #region 删除代理信息
        [PermissionFilter("Agent", "Index",Operationype.Delete)]
        public ActionResult DelUserByIDs()
        {
            string Ids = Request["id"] == null ? "" : Request["id"];
            if (!string.IsNullOrEmpty(Ids))
            {
                return OperationReturn(DALUtility.Agent.DeleteUser(Ids));
            }
            else
            {
                return OperationReturn(false);
            }
        }
        #endregion
        #region 获取单个代理信息
        public ActionResult GetFormJson(int id)
        {
            var agent = DALUtility.Agent.GetUserByUserId(id);
            return Content(JsonConvert.SerializeObject(agent));
        }
        #endregion
    }
}