using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;

namespace XM.Web.Controllers
{
    public class JuriMenuController : BaseController
    {
        // GET: JuriMenu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetALLRoleInfo()
        {
            string sort = Request["sort"] == null ? "RoleID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            string roleName = Request["role_name"] == null ? "" : Request["role_name"];
            int jurisdiction = Request["jurisdiction_id"] == null ? 1 : Convert.ToInt32(Request["jurisdiction_id"]);

            int totalCount;
            Dictionary<string, object> paras = new Dictionary<string, object>();

            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["sort"] = sort;
            paras["order"] = order;
            paras["RoleName"] = roleName;
            paras["Jurisdiction"] = jurisdiction;

            var roles = DALUtility.Role.QryRole<RoleEntity>(paras, out totalCount);

            if (roles != null)
            {
                log(HttpContext.Session["user_AN"].ToString(), "查询所有角色", "true", "查询成功");
            }
            else
            {
                log(HttpContext.Session["user_AN"].ToString(), "查询所有角色", "false", "查询失败");
            }
            return PagerData(totalCount, roles);
        }
        public ActionResult AddRole()
        {
            return View();
        }
        public ActionResult RoleAdd()
        {
            return SaveRole();
        }
        public ActionResult EditRole()
        {
            return View();
        }
        public ActionResult RoleEdit()
        {
            return SaveRole();
        }
        private ActionResult SaveRole()
        {
            int id = Convert.ToInt32(Request["id"]);
            string roleName = Request["role_name"];
            int jurisdiction = Convert.ToInt32(Request["jurisdiction_id"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["user_AN"] = roleName;
            paras["user_email"] = jurisdiction;
            int num;
            if (id == 0)
            {
                num = DALUtility.User.Save(paras);
                if (num > 0)
                {
                    log(HttpContext.Session["user_AN"].ToString(), "添加角色", "true", "添加成功");
                    return OperationReturn(true, "添加成功！");
                }
                else
                {
                    log(HttpContext.Session["user_AN"].ToString(), "添加用户", "false", "添加失败");
                    return OperationReturn(false, "添加失败！");
                }
            }
            num = DALUtility.User.Save(paras);
            if (num > 0)
            {
                log(HttpContext.Session["user_AN"].ToString(), "修改角色信息", "true", "修改成功");
                return OperationReturn(true, "修改成功！");
            }
            else
            {
                log(HttpContext.Session["user_AN"].ToString(), "修改角色信息", "false", "修改失败");
                return OperationReturn(false, "修改失败！");
            }
        }
    }
}