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
    /// 创建时间：2019/04/22
    /// 商品类型
    /// </summary>
    public class TypeController : BaseController
    {
        #region  类型页面
        //[PermissionFilter]
        // GET: Type
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region  获取所有类型信息
        //[PermissionFilter("Type", "Index")]
        public ActionResult GetAllTypeInfo()
        {
            string sort = Request["order"] == null ? "TypeID" : Request["order"];
            string order = Request["sort"] == null ? "asc" : Request["sort"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string typeName = Request["type_name"] == null ? "" : Request["type_name"];



            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["type_name"] = typeName;
            paras["sort"] = sort;
            paras["order"] = order;
            var type = DALUtility.Type.QryType<GoodsTypeEntity>(paras, out totalCount);
            return PagerData(totalCount, type,pageindex,pagesize);
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
            int id = Request["id"] == "" ? 0 : Convert.ToInt32(Request["id"]);
            string typeName = Request["TypeName"];
            int num;
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["type_name"] = typeName;
            if (id == 0)
            {
                num = DALUtility.Type.Save(paras);
                if (num > 0)
                {
                    return OperationReturn(true, "添加成功！");
                }
                else
                {
                    return OperationReturn(false, "添加失败！");
                }
                
            }
            num = DALUtility.Type.Save(paras);
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
        #region  删除操作
        //[PermissionFilter("Type", "Index",Operationype.Delete)]
        public ActionResult DelTypeByIDs()
        {
            string Ids = Request["id"] == null ? "" : Request["id"];
            if (!string.IsNullOrEmpty(Ids))
            {
                return OperationReturn(DALUtility.Type.DeleteType(Ids), "删除成功！");
            }
            else
            {
                return OperationReturn(false, "删除失败！");
            }
        }
        #endregion
        #region  获取类型信息
        public ActionResult GetFormJson(string id)
        {
            var vip = DALUtility.Type.GetTypeById(id);
            return Content(JsonConvert.SerializeObject(vip));
        }
        #endregion
    }
}