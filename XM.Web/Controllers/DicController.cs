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
    ///  字典参数
    ///  创建人: zxy
    ///  创建时间: 2019年5月23日
    /// </summary>
    public class DicController : BaseController
    {

        [PermissionFilter]
        public ActionResult Index()
        {
            ViewData["list"] = DALUtility.Dic.GetDicByTag(0).ToList();
            return View();
        }

        #region  添加/修改页面
        [PermissionFilter("Dic", "Index")]
        public ActionResult Form(int id=0,int tag=0)
        {
            DicEntity entity = new DicEntity();
            entity.tag = tag;
            if (id > 0)
            {
                entity = DALUtility.Dic.GetDicById(id);
            }
            ViewData["list"] = DALUtility.Dic.GetDicByTag(0).ToList();
            ViewData["entity"] = entity;
            return View("_Form");
        }
        #endregion

        [PermissionFilter("Dic", "Index")]
        public ActionResult GetGridJson(int tag = 0)
        {
            var roles = DALUtility.Dic.GetDicByTag(tag).ToList();
            var data = new { rows = roles };
            return Content(JsonConvert.SerializeObject(data));
        }

        #region  添加/修改操作
        [PermissionFilter("Dic", "Index", Operationype.Add)]
        public ActionResult Save(DicEntity entity)
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = entity.id;
            paras["name"] = entity.name;
            paras["tag"] = entity.tag;
            paras["sort"] = entity.sort;
            paras["code"] = entity.code;
            return OperationReturn(DALUtility.Dic.Save(paras) > 0);
        }
        #endregion

        #region  删除操作
        [PermissionFilter("Dic", "Index", Operationype.Delete)]
        public ActionResult Delete(int id)
        {
            bool boo = DALUtility.Dic.DeleteDic(id.ToString());
            return OperationReturn( boo, boo==true?"删除成功":"删除失败");
        }
        #endregion

    }
}