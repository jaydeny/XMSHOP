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
        public ActionResult Form()
        {
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
            Debug.Write(paras["rolemenu"] == null);
            return OperationReturn(DALUtility.Role.Save(paras) == 0);

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
        }
        #endregion

        /// <summary>
        ///  获取所有选单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllMenu(string roleId)
        {
            IEnumerable<RoleMenuEntity> rolemenuList = new List<RoleMenuEntity>();
            if (roleId != "")
            {
                // 当前角色菜单
                int myMenuCount;
                Dictionary<string, object> roleMenu = new Dictionary<string, object>();
                roleMenu["roleId"] = roleId;
                rolemenuList = DALUtility.RoleMenu.QryAllRoleMenu<RoleMenuEntity>(roleMenu, out myMenuCount);
            }
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = 1;
            paras["pageSize"] = 100;
            paras["order"] = "asc";
            paras["sort"] = "id";
            // 所有菜单
            int allMenuCount;
            IEnumerable<MenuEntity> allMenu = DALUtility.Menu.GetAllMenu<MenuEntity>(paras, out allMenuCount);
            ArrayList list = new ArrayList();
            bool checkstate;
            return Content(JsonConvert.SerializeObject(RoleTree(allMenu.ToList(), rolemenuList.ToList(), out checkstate,0)));
        }

        #region 角色树列图
        /// <summary>
        ///  选单树列图
        /// </summary>
        /// <param name="menuList">所有选单</param>
        /// <param name="roleMenusList">用户选单</param>
        /// <param name="checkstate">上一级选中状态</param>
        /// <param name="parentId">父节点</param>
        /// <returns></returns>
        public List<TreeViewModel> RoleTree(List<MenuEntity> menuList, List<RoleMenuEntity> roleMenusList, out bool checkstate, int parentId = 0) {
            checkstate = false;
            // 查询符合条件的记录
            List<MenuEntity> item = menuList.FindAll(t => t.ParentId == parentId);
            List<TreeViewModel> listTreeView = new List<TreeViewModel>();
            bool boo = true;
            for (var i = 0; i < item.Count(); i++)
            {
                TreeViewModel treeView = new TreeViewModel();
                treeView.complete = true;
                treeView.hasChildren = true;
                treeView.id = item[i].Id.ToString();
                treeView.parentnodes = item[i].Id.ToString();
                treeView.isexpand = true;
                treeView.parentnodes = parentId.ToString();
                treeView.showcheck = true;
                treeView.text = item[i].Name;
                treeView.value = "";
                treeView.checkstate = 0;
                bool checkState = false;
                if (menuList.FindAll(t => t.ParentId == item[i].Id).Count > 0)// 中层选单
                {
                    
                    treeView.ChildNodes = RoleTree(menuList, roleMenusList, out checkState, item[i].Id);
                }
                else
                {
                    boo = true;
                    for (int j = 0; j < roleMenusList.Count(); j++)
                    {
                        if (item[i].Id == roleMenusList[j].MenuId)
                        {
                            checkstate = true;
                            treeView.checkstate = 1;
                            treeView.ChildNodes = Operation(item[i].Id.ToString(), roleMenusList[j]);
                            boo = false;
                            break;
                        }
                    }
                    if (boo)
                    {
                        // 最底层赋予(添、改、删、其他)操作
                        treeView.ChildNodes = Operation(item[i].Id.ToString(), null);
                    }
                }
                if (checkState)
                {
                    treeView.checkstate = 1;
                }
                listTreeView.Add(treeView);
            }
            return listTreeView;
        }
        
        /// <summary>
        ///  基础选单
        /// </summary>
        /// <param name="id">选单编号</param>
        /// <param name="roleMenu">选单对象</param>
        /// <returns></returns>
        public List<TreeViewModel> Operation(string id, RoleMenuEntity roleMenu)
        {
            string[] arrayName = new string[4] { "添加", "修改", "删除", "其他" };
            string[] arrayValue = new string[4] { "NF-add", "NF-edit", "NF-delete", "NF-Details" };
            int[] checkstateArray = null;
            if (roleMenu != null)
            {
                checkstateArray = new int[4] { roleMenu.RmAdd, roleMenu.RmUpdate, roleMenu.RmDelete, roleMenu.RmOther };
            }
            List<TreeViewModel> list = new List<TreeViewModel>();
            TreeViewModel treeView;
            for (int i=0; i<4; i++)
            {
                treeView = new TreeViewModel();
                // 选中状态
                treeView.checkstate = roleMenu == null ? 0: checkstateArray[i];
                // 父节点
                treeView.parentId = id;
                treeView.complete = true;
                treeView.hasChildren = false;
                treeView.isexpand = true;
                treeView.showcheck = true;
                treeView.text = arrayName[i];
                treeView.value = arrayValue[i];
                treeView.ChildNodes = new List<TreeViewModel>();
                // 编号Id
                treeView.id = id+"-"+i;
                treeView.parentnodes = id;
                list.Add(treeView);
            }
            return list;
        }
        #endregion
        

    }
}