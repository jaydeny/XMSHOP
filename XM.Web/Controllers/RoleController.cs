using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        public ActionResult Save()
        {
            int id = Convert.ToInt32(Request["id"]);
            string Name = Request["name"];
            int state = Convert.ToInt32(Request["state"]);
            string code = Request["code"];
            int menuId = Convert.ToInt32(Request["MenuId"]);
            int RmAdd = Convert.ToInt32(Request["RmAdd"]);
            int RmUpdate = Convert.ToInt32(Request["RmUpdate"]);
            int RmDelete = Convert.ToInt32(Request["RmDelete"]);
            int RmOther = Convert.ToInt32(Request["RmOther"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["name"] = Name;
            paras["state"] = state;
            paras["code"] = code;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("MenuId", typeof(int));
            dataTable.Columns.Add("RmAdd", typeof(bool));
            dataTable.Columns.Add("RmUpdate", typeof(bool));
            dataTable.Columns.Add("RmDelete", typeof(bool));
            dataTable.Columns.Add("RmOther", typeof(bool));
            DataRow dr1 = dataTable.NewRow();
            dr1[0] = id;
            dr1[1] = menuId;
            dr1[2] = RmAdd;
            dr1[3] = RmUpdate;
            dr1[4] = RmDelete;
            dr1[5] = RmOther;
            dataTable.Rows.Add(dr1);
            paras["rolemenu"] = dataTable;
            int num;
            if (id == 0)
            {
                num = DALUtility.User.Save(paras);
                if (num > 0)
                {
                    return OperationReturn(true, "添加成功！");
                }
                else
                {
                    return OperationReturn(false, "添加失败！");
                }
            }
            num = DALUtility.User.Save(paras);
            if (num > 0)
            {
                return OperationReturn(true, "修改成功！");
            }
            else
            {
                return OperationReturn(false, "修改失败！");
            }
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
            var vip = DALUtility.Role.GetRoleById(id);
            return Content(JsonConvert.SerializeObject(vip));
        }
        #endregion
    }
}