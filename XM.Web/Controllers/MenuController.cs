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
    public class MenuController : BaseController
    {
        #region  菜单页面
        //[PermissionFilter]
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region  获取所有菜单信息
        //[PermissionFilter("Menu", "Index")]
        public ActionResult GetAllMenu()
        {
            string sort = Request["order"] == null ? "id" : Request["order"];
            string order = Request["sort"] == null ? "asc" : Request["sort"];

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
            paras["name"] = name;
            paras["sort"] = sort;
            paras["order"] = order;
            var menus = DALUtility.Menu.GetAllMenu<MenuEntity>(paras, out totalCount);
            return PagerData(totalCount, menus,pageindex,pagesize);
        }
        #endregion
        #region  添加/修改菜单页面
        public ActionResult Form()
        {
            return View("_Form");
        }
        #endregion
        #region  添加/修改菜单信息
        public ActionResult Save()
        {
            int id = Request["id"] == "" ? 0 : Convert.ToInt32(Request["id"]);
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
            if (id == 0)
            {
                return OperationReturn(DALUtility.Menu.Save(paras) > 0);
            }
            return OperationReturn(DALUtility.Menu.Save(paras) > 0);
        }
        #endregion
        #region 删除菜单信息
        //[PermissionFilter("Menu", "Index", Operationype.Delete)]
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
        #endregion
        #region 获取单个菜单信息
        public ActionResult GetFormJson(string id)
        {
            var menu = DALUtility.Menu.GetMenuById(id);
            return Content(JsonConvert.SerializeObject(menu));
        }
        #endregion
    }
}