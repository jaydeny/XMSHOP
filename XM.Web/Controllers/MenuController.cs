using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    public class MenuController : BaseController
    {
        [PermissionFilter]
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        [PermissionFilter("Menu", "Index")]
        public ActionResult GetAllMenu()
        {
            string sort = Request["sort"] == null ? "id" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string name = Request["name"] == null ? "" : Request["name"];
            string code = Request["code"] == null ? "" : Request["code"];
            string controller = Request["controller"] == null ? "" : Request["controller"];
            string action = Request["action"] == null ? "" : Request["action"];
            int parentid = Request["parentid"] == null ? 10 : Convert.ToInt32(Request["parentid"]);
            int state = Request["state"] == null ? 10 : Convert.ToInt32(Request["state"]);
            int sortvalue = Request["sortvalue"] == null ? 10 : Convert.ToInt32(Request["sortvalue"]);


            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["AgentName"] = name;
            paras["sort"] = sort;
            paras["order"] = order;
            var users = DALUtility.Agent.QryUsers<MenuEntity>(paras, out totalCount);
            return PagerData(totalCount, users);
        }
        public ActionResult AddMenu()
        {
            return View("_AddMenu");
        }
        [PermissionFilter("Menu", "Index",Operationype.Add)]
        public ActionResult MenuAdd()
        {
            return Save();
        }
        
        public ActionResult EditMenu()
        {
            return View("_EditMenu");
        }
        [PermissionFilter("Menu", "Index", Operationype.Update)]
        public ActionResult MenuEdit()
        {
            return Save();
        }

        public ActionResult Save()
        {
            int id = Request["id"] == null ? 1 : Convert.ToInt32(Request["id"]);
            string name = Request["name"] == null ? "" : Request["name"];
            string code = Request["code"] == null ? "" : Request["code"];
            string controller = Request["controller"] == null ? "" : Request["controller"];
            string action = Request["action"] == null ? "" : Request["action"];
            int parentid = Request["parentid"] == null ? 10 : Convert.ToInt32(Request["parentid"]);
            int state = Request["state"] == null ? 10 : Convert.ToInt32(Request["state"]);
            int sortvalue = Request["sortvalue"] == null ? 10 : Convert.ToInt32(Request["sortvalue"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["name"] = name;
            paras["code"] = code;
            paras["controller"] = controller;
            paras["action"] = action;
            paras["parentid"] = parentid;
            paras["state"] = state;
            paras["sortvalue"] = sortvalue;

            return OperationReturn(DALUtility.Menu.Save(paras) > 0);
        }
        [PermissionFilter("Menu", "Index", Operationype.Delete)]
        public ActionResult DelMenuByIDs()
        {
            string Ids = Request["id"] == null ? "" : Request["id"];
            if (!string.IsNullOrEmpty(Ids))
            {
                return OperationReturn(DALUtility.Menu.DeleteMenu(Ids), "删除成功");
            }
            else
            {
                return OperationReturn(false, "删除失败");
            }
        }
    }
}