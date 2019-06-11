using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using XM.Comm;
using XM.DALFactory;
using XM.Model;

namespace XM.WebVIP.Controllers
{
    public class BaseController : Controller
    {

        //购物车项
       public static Hashtable cartTable = new Hashtable();
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
        
        protected string GameReturn(string _action,string _key,string[] _paras,string _culture = "zh-cn")
        {
            return JsonConvert.SerializeObject(new { action = _action, key = _key, paras = _paras, culture = _culture });

        }
        
        protected static string GameReturnS(string _action, string _key, string[] _paras, string _culture = "zh-cn")
        {
            return JsonConvert.SerializeObject(new { action = _action, key = _key, paras = _paras, culture = _culture });

        }

        /// <summary>
        /// 功能:记录会员端的信息
        /// </summary>
        //会员账号
        public string AN { get { return Session["AN"].ToString(); } }
        //密码
        public string PWD { get { return Session["PWD"].ToString(); } }
        //id
        public string ID { get { return Session["id"].ToString(); } }
        //余额
        public decimal Remainder { get { return decimal.Parse(Session["Remainder"].ToString()); } }
        //积分
        public static string Integral;

        /// <summary>
        /// 记录代理的信息
        /// </summary>
        //代理id
        public string Agent_ID { get { return Session["Agent_ID"].ToString(); } }
        //代理账号
        public string Agent_Acc { get { return Session["Agent_Acc"].ToString(); } }
    }
}
