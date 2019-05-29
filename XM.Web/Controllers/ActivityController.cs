using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;

namespace XM.Web.Controllers
{
    public class ActivityController : BaseController
    {
        // GET: Activity
        public ActionResult Index()
        {
            return View();
        }
        //获取当前后台用户所发布的活动
        public ActionResult  getAllActtvity()
        {
            string sort = Request["order"] == null ? "id" : Request["order"];
            string order = Request["sort"] == null ? "asc" : Request["sort"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["Publisher"] = "admin";
            paras["sort"] = sort;
            paras["order"] = order;
            var users = DALUtility.Activity.getAllActivity<ActivityEntity>(paras, out totalCount);
            return PagerData(totalCount, users, pageindex, pagesize);
        }
        
        // 添加活动页面
        public ActionResult ActivityAdd()
        {
            DateTime dt = DateTime.Now;
            string StartWeek = dt.ToString("yyyy-MM-dd"); //获取一周的开始日期
            string EndWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).AddDays(6).ToString("yyyy-MM-dd"); //获取本周星期天日期

            ViewData["StartWeek"] = StartWeek;
            ViewData["EndWeek"] = EndWeek;
            return View();
        }
        
        //获取活动优惠类型
        public string  ActivityType()
        {
            var ActDic = DALUtility.Dic.GetDicByTag(17);
            return JsonConvert.SerializeObject(ActDic);
        }

        public ActionResult Activity4Add() {
            
            UserEntity user = Session["User"] as UserEntity;
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
            paras["receiver"] = Request["receiver"];
            //发布人
            paras["publisher"] = user.UserAccountName;
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

            var aa = DALUtility.Activity.AddActivity(paras);

            return OperationReturn(true, "发布公告成功!");
        }

    }
}