using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{
    public class ActivityController : BaseController
    {
        #region _View
        /// <summary>
        /// 返回活动页
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivityIndex()
        {
            return View();
        }

        /// <summary>
        /// 返回活动记录页
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivityRecord()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// 获取活动优惠类型
        /// </summary>
        /// <returns></returns>
        public string ActivityType()
        {
            var ActDic = DALUtility.Dic.GetDicByTag(17);
            return JsonConvert.SerializeObject(ActDic);
        }

        /// <summary>
        /// 获取当前代理用户所发布的活动
        /// </summary>
        /// <returns></returns>
        public ActionResult getAllActtvity()
        {
            string sort = Request["order"] == null ? "id" : Request["order"];
            string order = Request["sort"] == null ? "asc" : Request["sort"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["Publisher"] = Session["agent_AN"].ToString();
            paras["sort"] = sort;
            paras["order"] = order;
            var users = DALUtility.Activity.getAllActivity<ActivityEntity>(paras, out totalCount);
            return PagerData(totalCount, users, pageindex, pagesize);
        }


        /// <summary>
        /// 添加活动 
        /// </summary>
        /// <returns></returns>
        public ActionResult Activity4Add()
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            //标题
            paras["title"] = Request["title"];
            //内容
            paras["content"] = Request["content"];
            //创建时间
            paras["createTime"] = DateTime.Now;
            //活动开始时间
            paras["startTime"] = Request["StartDate"];
            //活动结束时间
            paras["endTime"] = Request["EndDate"];
            //接收人
            paras["receiver"] = Session["agent_AN"].ToString();
            //发布人
            paras["publisher"] = Session["agent_AN"].ToString();
            //活动类型
            paras["actype"] = Request["typeNum"];
            //满额
            paras["full"] = Request["full"];
            //减额
            paras["minus"] = Request["minus"];
            //折扣
            paras["discount"] = Request["discount"];
            //次数
            paras["count"] = Request["count"];

            var res = DALUtility.Activity.AddActivity(paras);

            if (res == 1)
                return OperationReturn(true, "添加折扣活动成功!");
            else if (res == 0)
                return OperationReturn(true, "添加满减活动成功!");
            return OperationReturn(false, "发布活动失败!");
        }


    }
}