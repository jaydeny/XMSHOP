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
            string goodsName = Request["GoodsName"];
            string goodsIntro = Request["GoodsIntro"];
            decimal goodsPrice = Convert.ToDecimal(Request["GoodsPrice"]);
            string createBy = user.UserAccountName;
            string goodsPic = Request["GoodsPicture"];
            int typeId = Convert.ToInt32(Request["GoodsType"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["goods_name"] = goodsName;
            paras["goods_intro"] = goodsIntro;
            paras["goods_CP"] = goodsPrice;
            paras["type_id"] = typeId;
            paras["goods_pic"] = goodsPic;
            if (id == 0)
            {
                paras["goods_CBY"] = createBy;
                paras["goods_CDT"] = DateTime.Now;
                return OperationReturn(DALUtility.Goods.Save(paras) > 0);
            }
            return OperationReturn(DALUtility.Goods.Save(paras) > 0);

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