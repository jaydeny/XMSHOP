/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    /// <summary>
    /// 商品功能
    /// </summary>
    public class ProductController : BaseController
    {
        #region _goods
        /// <summary>
        /// 功能:查询所有的代理商商品
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryAgoods()
        {
            string sort = Request["sort"] == null ? "id" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("order", order);
            param.Add("goods_Name", Request["goods_Name"]);
            param.Add("status_id", 3);
            param.Add("type_id", Request["type_id"]);
            param.Add("agent_AN", Session["Agent_Acc"] != null ? Session["Agent_Acc"].ToString() : "agent0");

            string result = DALUtility.Agent.QryAgoods(param, out int ICount);
            return Content(result);
        }

        /// <summary>
        /// 功能:查询所有的商品
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult GetAllGoodsInfo()
        {
            string sort = Request["sort"] == null ? "GoodsID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];
            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string goodsName = Request["goods_name"] == null ? "" : Request["goods_name"];
            string goodsIntro = Request["goods_intro"] == null ? "" : Request["goods_intro"];
            decimal goodsPrice = Request["goods_CP"] == null ? 1 : Convert.ToDecimal(Request["goods_CP"]);
            string createBy = Request["goods_BY"] == null ? "" : Request["goods_BY"];
            string createDateTime = Request["goods_CDT"] == null ? "" : Request["goods_CDT"];
            string goodsPic = Request["goods_pic"] == null ? "" : Request["goods_pic"];
            int typeId = Request["type_id"] == null ? 1 : Convert.ToInt32(Request["type_id"]);

            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["goods_name"] = goodsName;
            paras["sort"] = sort;
            paras["order"] = order;
            var goods = DALUtility.Goods.QryGoods<GoodsEntity>(paras, out totalCount);
            return PagerData(totalCount, goods);
        }

        /// <summary>
        /// 功能：返回商品筛选页
        /// </summary>
        public ActionResult AgoodsList()
        {
            ViewData["VipAccountName"] = Session["AN"];
            List<DicEntity> list = DALUtility.Dic.GetDicByTag(15).ToList();
            ViewData["AGoodsType"] = list;
            return View();
        }

        /// <summary>
        /// 功能:查询热销单品和精品商品
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult HotGoods()
        {
            string sort = Request["sort"] == null ? "price" : Request["sort"];
            string order = Request["order"] == null ? "desc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 8 : Convert.ToInt32(Request["rows"]);


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("order", order);
            param.Add("agent_AN", Session["Agent_Acc"] != null ? Session["Agent_Acc"].ToString() : "agent0");

            string result = DALUtility.Agent.QryAgoods(param, out int ICount);
            return Content(result);
        }

        /// <summary>
        /// 功能:查询热销单品和精品商品
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult BoutiqueGoods()
        {
            string sort = Request["sort"] == null ? "price" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 6 : Convert.ToInt32(Request["rows"]);


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("order", order);
            param.Add("agent_AN", Session["Agent_Acc"] != null ? Session["Agent_Acc"].ToString() : "agent0");

            string result = DALUtility.Agent.QryAgoods(param, out int ICount);
            return Content(result);
        }
        #endregion
    }
}