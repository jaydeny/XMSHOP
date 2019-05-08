using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Domain;
using XM.Web.Models;

namespace XM.Web.Controllers
{
    public class RoleController : BaseController
    {
        #region  角色页面
        //[PermissionFilter]
        // GET: JuriMenu
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region  获取所有角色信息
        //[PermissionFilter("Role", "Index")]
        public ActionResult GetALLRoleInfo()
        {
            string sort = Request["order"] == null ? "ID" : Request["order"];
            string order = Request["sort"] == null ? "asc" : Request["sort"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            string roleName = Request["name"] == null ? "" : Request["name"];
            int state = Request["state"] == null ? 1 : Convert.ToInt32(Request["state"]);
            string code = Request["code"] == null ? "" : Request["code"];

            int totalCount;
            Dictionary<string, object> paras = new Dictionary<string, object>();

            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["sort"] = sort;
            paras["order"] = order;
            paras["Name"] = roleName;
            paras["State"] = state;
            paras["Code"] = code;


            var roles = DALUtility.Role.QryRole<RoleEntity>(paras, out totalCount);
            return PagerData(totalCount, roles,pageindex,pagesize);
        }
        #endregion
        #region  添加/修改页面
        public ActionResult Form( string id)
        {
            Debug.WriteLine(id==null);
            //if (id !=0)
            //{
            //    // 角色信息
            //    var role = DALUtility.Role.GetRoleById(id.ToString());
            //    // 当前角色所选的权限
            //    int myMenuCount;
            //    Dictionary<string, object> roleMenu = new Dictionary<string, object>();
            //    roleMenu["roleId"] = id;
            //    DALUtility.RoleMenu.QryAllRoleMenu<RoleEntity>(roleMenu,out myMenuCount);
            //}
            return View("_Form");
        }
        #endregion
        #region  添加/修改操作
        public ActionResult Save(RoleEntity roleEntity)
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = roleEntity.Id;
            paras["name"] = roleEntity.Name;
            paras["state"] = roleEntity.State;
            paras["code"] = roleEntity.Code;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MenuId", typeof(int));
            dataTable.Columns.Add("RmAdd", typeof(bool));
            dataTable.Columns.Add("RmUpdate", typeof(bool));
            dataTable.Columns.Add("RmDelete", typeof(bool));
            dataTable.Columns.Add("RmOther", typeof(bool));
            foreach (var itm in roleEntity.roleMemus)
            {
                DataRow dr1 = dataTable.NewRow();
                dr1[0] = itm.m_id;
                dr1[1] = itm.add;
                dr1[2] = itm.update;
                dr1[3] = itm.delete;
                dr1[4] = itm.other;
                dataTable.Rows.Add(dr1);
            }
            paras["rolemenu"] = dataTable;
            return OperationReturn(DALUtility.Role.Save(paras) > 0);

        }
        #endregion
        #region 删除操作
        //[PermissionFilter("Role", "Index", Operationype.Delete)]
        public ActionResult DelRoleByIds()
        {
            string Ids = Request["id"] == null ? "" : Request["id"];
            if (!string.IsNullOrEmpty(Ids))
            {
                return OperationReturn(DALUtility.Role.DeleteRole(Ids), "删除成功");
            }
            else
            {
                return OperationReturn(false, "删除失败");
            }
        }
        #endregion
        #region  角色详细信息
        public ActionResult GetFormJson(string id)
        {
            var role = DALUtility.Role.GetRoleById(id);
            return Content(JsonConvert.SerializeObject(role));
            // 角色信息
            //var role = DALUtility.Role.GetRoleById(id);
            // 当前角色所选的权限
            //int myMenuCount;
            //Dictionary<string, object> roleMenu = new Dictionary<string, object>();
            // roleMenu["r_id"] = id;
            //var rolemenuList = DALUtility.RoleMenu.QryAllRoleMenu<RoleMenuEntity>(roleMenu, out myMenuCount);
            //return Content(JsonConvert.SerializeObject(new { role = role, rolemenuList = rolemenuList, roleMenuCount= myMenuCount } ));
        }
        #endregion

        /// <summary>
        ///  获取所有选单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllMenu()
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = 1;
            paras["pageSize"] = 100;
            paras["order"] = "asc";
            paras["sort"] = "id";
            // 所有选单
            int allMenuCount;
            IEnumerable<MenuEntity> allMenu = DALUtility.Menu.GetAllMenu<MenuEntity>(paras, out allMenuCount);
            //return Content(JsonConvert.SerializeObject(allMenu));
            ArrayList list = new ArrayList();
            return Content(JsonConvert.SerializeObject(Operation(allMenu.ToList())));
        }

        public List<TreeViewModel> Operation(List<MenuEntity> menuList) {
            List<TreeViewModel> list = new List<TreeViewModel>();
            TreeViewModel treeView;
            for ( int i=0; i< menuList.Count(); i++)
            {
                treeView = new TreeViewModel();
                treeView.checkstate = 0;
                treeView.complete = true;
                treeView.hasChildren = true;
                treeView.id = menuList[i].Id.ToString();
                treeView.isexpand = true;
                treeView.parentnodes = "";
                treeView.showcheck = true;
                treeView.text = menuList[i].Name;
                treeView.value = "";
                treeView.ChildNodes = Operation(menuList[i].Id.ToString());
                list.Add(treeView);
            }
            return list;

        }


        /// <summary>
        ///  单选操作
        /// </summary>
        /// <returns></returns>
        public List<TreeViewModel> Operation(string id)
        {
            string[] arrayName = new string[4] { "添加", "修改", "删除", "查看机构" };
            string[] arrayValue = new string[4] { "NF-add", "NF-edit", "NF-delete", "NF-Details" };
            List<TreeViewModel> list = new List<TreeViewModel>();
            TreeViewModel treeView;
            for (int i=0; i<4; i++)
            {
                treeView = new TreeViewModel();
                treeView.checkstate = 0;
                treeView.parentId = id;
                treeView.complete = true;
                treeView.hasChildren = false;
                treeView.isexpand = true;
                treeView.showcheck = true;
                treeView.text = arrayName[i];
                treeView.value = arrayValue[i];
                treeView.ChildNodes = new List<TreeViewModel>();
                //treeView.id = Guid.NewGuid().ToString();
                treeView.id = id+"-"+i;
                treeView.parentnodes = id;
                list.Add(treeView);
            }
            return list;
        }

    }
}