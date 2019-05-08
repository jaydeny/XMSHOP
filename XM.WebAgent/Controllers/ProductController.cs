using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{

    /// <summary>
    /// 商品相关的方法,上架,定价,查询所有商品,查询所有代理端商品
    /// </summary>
    public class ProductController : BaseController
    {
        #region _Product
        /// <summary>
        /// 作者:梁钧淋
        /// 日期:2019/4/26
        /// 功能:返回商品页面
        /// </summary>
        /// <returns></returns>
        //返回商品操作页
        public ActionResult getProductPage()
        {
            return View();
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行商品上架或者修改
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult MakeGoods()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", Request["Agoods_id"] == null ? "0":Request["Agoods_id"]);
            param.Add("goods_id", Request["goods_id"]);
            param.Add("status_id", Request["status_id"]);
            param.Add("price", Request["price"]);
            param.Add("up_time", DateTime.Now); 
            param.Add("Agent_AN", Session["Agent_AN"].ToString());
            param.Add("goods_name", Request["goods_name"]);

            int iCheck = DALUtility.Agent.MakeGoods(param);
            if (iCheck != 2)
            {
                return OperationReturn(true, iCheck == 0 ?"上架成功" : "修改成功");
            }
            return OperationReturn(false, "上架失败");
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:查询所有的代理商商品
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryAgoods()
        {
            string sort = Request["sort"] == null ? "id" : Request["sort"];
            string order = Request["order"] == null ? "desc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("order", order);
            //param.Add("status_id", 1);
            param.Add("goods_name", Request["goods_name"]);
            param.Add("status_id", 1);
            param.Add("agent_AN", Session["Agent_AN"] != null ? Session["Agent_AN"].ToString() : "agent");

            string result = DALUtility.Agent.QryAgoods(param, out int ICount);
            return Content(result);
        }


        /// <summary>
        /// 查询所有的商品
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult GetAllGoodsInfo()
        {
            string sort = Request["sort"] == null ? "GoodsID" : Request["sort"];
            string order = Request["order"] == null ? "desc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            
            //int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["goods_name"] = Request["goods_name"];
            paras["sort"] = sort;
            paras["order"] = order;
            var goods = DALUtility.Agent.QryGoods(paras, out int ICount);
            return Content(goods);
        }

        #endregion
        
    }
}