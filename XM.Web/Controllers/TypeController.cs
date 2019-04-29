using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;

namespace XM.Web.Controllers
{
    public class TypeController : BaseController
    {
        // GET: Type
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllTypeInfo()
        {
            string sort = Request["sort"] == null ? "TypeID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

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
            return PagerData(totalCount, type);
        }
        public ActionResult TypeAdd()
        {
            return View("_TypeAdd");
        }

        /// <summary>
        /// 新增 类型
        /// </summary>
        /// <returns></returns>
        public ActionResult AddType()
        {
            return SaveType();

        }

        public ActionResult TypeEdit()
        {
            return View("_TypeEdit ");
        }
        /// <summary>
        /// 编辑 类型
        /// </summary>
        /// <returns></returns>
        public ActionResult EditType()
        {

            return SaveType();
        }

        private ActionResult SaveType()
        {
            int id = Convert.ToInt32(Request["id"]);
            string typeName = Request["type_name"];
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
    }
}