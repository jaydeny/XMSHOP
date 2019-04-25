using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;

namespace XM.Web.Controllers
{
    public class VipController : BaseController
    {
        // GET: Vip
        public ActionResult Index()
        {

            return View();
        }
        
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


        /// <summary>
        /// 新增 用户
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
        {
            return SaveUser();

        }


        /// <summary>
        /// 编辑 用户
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUser()
        {

            return SaveUser();
        }

        private ActionResult SaveUser()
        {
            int id = Convert.ToInt32(Request["id"]);
            string userid = Request["vip_AN"];
            string mobilephone = Request["vip_mp"];
            string email = Request["vip_email"];
            int statusID = Convert.ToInt32(Request["status_id"]);
            int agentId = Convert.ToInt32(Request["agent_id"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["vip_AN"] = userid;
            paras["vip_mp"] = mobilephone;
            paras["vip_email"] = email;


            int iCheck = DALUtility.Vip.CheckUseridAndEmail(paras);
            if (iCheck > 0)
            {
                log(HttpContext.Session["user_AN"].ToString(), "修改\\添加vip用户", "false", "用户名或邮箱重复 ");
                return OperationReturn(false, iCheck == 1 ? "用户名重复" : "邮箱重复");
            }
            else
            {
                int num;
                paras["status_id"] = statusID;
                paras["agent_id"] = agentId;
                if (id == 0)
                {
                    paras["vip_pwd"] = "xm123456";
                    paras["vip_CDT"] = DateTime.Now;
                    num = DALUtility.Vip.Save(paras);
                    if (num > 0)
                    {
                        log(HttpContext.Session["user_AN"].ToString(), "添加vip用户", "true", "添加成功");
                        return OperationReturn(true, "添加成功！初始密码：" + paras["vip_pwd"]);
                    }
                    else
                    {
                        log(HttpContext.Session["user_AN"].ToString(), "添加vip用户", "false", "添加失败");
                        return OperationReturn(false, "添加失败");
                    }
                    
                }
                num = DALUtility.Vip.Save(paras);
                if (num > 0)
                {
                    log(HttpContext.Session["user_AN"].ToString(), "修改vip用户", "true", "修改成功");
                    return OperationReturn(true, "修改成功！");
                }
                else
                {
                    log(HttpContext.Session["user_AN"].ToString(), "修改vip用户", "false", "修改失败 ");
                    return OperationReturn(false, "修改失败！");
                }
                
            }
        }

        public ActionResult DelUserByIDs()
        {
            string Ids = Request["id"] == null ? "" : Request["id"];
            if (!string.IsNullOrEmpty(Ids))
            {
                log(HttpContext.Session["user_AN"].ToString(), "删除vip用户", "true", "删除成功 ");
                return OperationReturn(DALUtility.Vip.DeleteUser(Ids),"删除成功");
            }
            else
            {
                log(HttpContext.Session["user_AN"].ToString(), "删除vip用户", "false", "删除失败 ");
                return OperationReturn(false,"删除失败");
            }
        }
    }
}