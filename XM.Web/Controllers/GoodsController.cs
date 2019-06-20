/*-------------------------------------*
 * 创建人:         朱茂琛
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       朱茂琛       创建
 *-------------------------------------*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 商品
    /// </summary>
    public class GoodsController : BaseController
    {
        #region 获取所有商品页面
        [PermissionFilter]
        // GET: Goods
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取所有商品信息
        [PermissionFilter("Goods", "Index")]
        public ActionResult GetAllGoodsInfo()
        {
            string sort = Request["order"] == null ? "GoodsID" : Request["order"];
            string order = Request["sort"] == null ? "asc" : Request["sort"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string goodsName = Request["GoodsName"] == null ? "" : Request["GoodsName"];
            string goodsIntro = Request["GoodsIntro"] == null ? "" : Request["GoodsIntro"];
            decimal goodsPrice = Request["GoodsPrice"] == null ? 1 : Convert.ToDecimal(Request["GoodsPrice"]);
            string createBy = Request["GoodsCreateBy"] == null ? "" : Request["GoodsCreateBy"];
            string createDateTime = Request["GoodsCreateTime"] == null ? "" : Request["GoodsCreateTime"];
            string goodsPic = Request["GoodsPicture"] == null ? "" : Request["GoodsPicture"];
            int typeId = Request["GoodsType"] == null ? 1 : Convert.ToInt32(Request["GoodsType"]);



            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["goods_name"] = goodsName;
            paras["sort"] = sort;
            paras["order"] = order;
            var goods = DALUtility.Goods.QryGoods<GoodsEntity>(paras, out totalCount);
            return PagerData(totalCount, goods,pageindex,pagesize);
        }
        #endregion

        #region  添加/修改页面
        public ActionResult Form()
        {
            return View("_Form");
        }
        #endregion

        #region  添加/修改商品信息
        [PermissionFilter("Goods", "Index",Operationype.Add)]
        public ActionResult Save()
        {
            UserEntity user = Session["User"] as UserEntity;
            int id = Request["id"] == "" ? 0 : Convert.ToInt32(Request["id"]);
            string goodsName = Request.Form["GoodsName"];
            int typeId = Convert.ToInt32(Request.Form["GoodsType"]);
            decimal goodsPrice = Convert.ToDecimal(Request.Form["GoodsPrice"]);
            string goodsIntro = Request.Form["GoodsIntro"];
            string createBy = user.UserAccountName;
            
            //获取上传的文件
            HttpPostedFileBase File = Request.Files["GoodsPicture"];
            //获取文件名:当前时间+文件名
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + File.FileName;
            
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["goods_name"] = goodsName;
            paras["goods_intro"] = goodsIntro;
            paras["goods_CP"] = goodsPrice;
            paras["type_id"] = typeId;
            paras["goods_pic"] = fileName;

            //"D:\\Shop\\img"是服务器路径,调试则修改成本地路径
            string serverPath = string.Format("{0}\\{1}", "D:\\Shop\\img", fileName);
            //文件存在,则删除
            if (System.IO.File.Exists(serverPath))
            {
                System.IO.File.Delete(serverPath);
            }
            //新建文件,写入用
            FileStream fs = new FileStream(serverPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fs.Close();
            Request.Files["GoodsPicture"].SaveAs(serverPath);

            //返回值用的两个参数
            List<int> resultList = new List<int>();
            string result = "";

            try
            {
                resultList.Add(0);
                result = "添加图片已成功";
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(serverPath);
                resultList.Add(1);
                result = "添加图片失败";
            }

            

            if (id == 0)
            {
                //添加操作的时候,需要额外的参数
                paras["goods_CBY"] = createBy;
                paras["goods_CDT"] = DateTime.Now;

                int iCheck = DALUtility.Goods.Save(paras);

                resultList.Add(iCheck > 0 ? 0 : 1);
                result+= iCheck > 0 ? ",操作成功" : ",操作失败";
            }
            else
            {
                int iCheck = DALUtility.Goods.Save(paras);

                resultList.Add(iCheck > 0 ? 0 : 1);
                result += iCheck > 0 ? ",操作成功" : ",操作失败";
            }

            if (resultList.Contains(1))
            {
                return OperationReturn(true,result);
            }
            return OperationReturn(false, result);
        }
        #endregion

        #region  删除商品
        [PermissionFilter("Goods", "Index", Operationype.Delete)]
        public ActionResult DelGoodsByIDs()
        {
            string Ids = Request["id"] == null ? "" : Request["id"];
            if (!string.IsNullOrEmpty(Ids))
            {
                return OperationReturn(DALUtility.Goods.DeleteGoods(Ids),"删除成功！");
            }
            else
            {
                return OperationReturn(false,"删除失败");
            }
        }
        #endregion

        #region 获取单个商品信息
        public ActionResult GetFormJson(string id)
        {
            var vip = DALUtility.Goods.QryGoodsInfo(id);
            return Content(JsonConvert.SerializeObject(vip));
        }
        #endregion
        
    }
}