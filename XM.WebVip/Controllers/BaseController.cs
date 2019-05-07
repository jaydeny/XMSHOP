using FrameWork.MongoDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XM.Comm;
using XM.DALFactory;
using XM.Model;

namespace XM.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 数据交互接口
        /// </summary>
        internal DALCore DALUtility => DALCore.GetInstance();

        protected ContentResult PagerData(int totalCount, object rows)
        {
            return Content(JsonConvert.SerializeObject(new { total = totalCount.ToString(), rows = rows }));
        }

        protected ContentResult OperationReturn(bool _success, string _msg = "")
        {
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success }));

        }

        protected ContentResult OperationReturn(bool _success, string _msg = "", object obj = null)
        {
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success, data = obj}));

        }

        public void log(string Operator, string Method, string boo, string reason)
        {
            var dbService = new MongoDbService();

            var id = Guid.NewGuid().ToString();
            dbService.Add(new LogEntity
            {
                _id = id,
                Operator = Operator,
                Method = Method,
                boo = boo,
                reason = reason,
                Time = DateTime.Now
            });
        } 
        public string AN { get { return Session["AN"].ToString(); } }
        public string ID { get { return Session["id"].ToString(); } }
        public string Agent_ID { get { return Session["Agent_ID"].ToString(); } }
        public string Agent_AN { get { return Session["Agent_AN "].ToString(); } }



        /// <summary>
        /// 在重写的Initialize方法(继承Controller的基类中)中不断的注册SessionId：
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Session["SessionId"] = Session.SessionID;
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;
            

            if (!SSOHelper.CheckOnline())
            {
                Url.Action("RemoveSession", "Home");
            }
        }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class result_base
    {
        public string errorCode { get; set; } = "";
        public string errorMsg { get; set; } = "";
        public object result { get; set; }
    }
}
