/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{

    /// <summary>
    ///  字典参数控制器
    /// </summary>
    public class DicController : BaseController
    {
        #region  view

        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        [PermissionFilter]
        public ActionResult Index()
        {
            ViewData["list"] = DALUtility.Dic.GetDicByTag(0).ToList();
            return View();
        }

        /// <summary>
        /// 添加/修改页面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 数据列表
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [PermissionFilter("Dic", "Index")]
        public ActionResult GetGridJson(int tag = 0)
        {
            var roles = DALUtility.Dic.GetDicByTag(tag).ToList();
            var data = new { rows = roles };
            return Content(JsonConvert.SerializeObject(data));
        }

        #region  添加/修改操作

        /// <summary>
        /// 添加/修改操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionFilter("Dic", "Index", Operationype.Delete)]
        public ActionResult Delete(int id)
        {
            bool boo = DALUtility.Dic.DeleteDic(id.ToString());
            return OperationReturn( boo, boo==true?"删除成功":"删除失败");
        }
        #endregion

    }
}