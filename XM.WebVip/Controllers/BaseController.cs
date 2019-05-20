using Newtonsoft.Json;
using System;
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

        public static Dictionary<string, string> pairs = new Dictionary<string, string>();
        public static Dictionary<string, bool> recycle = new Dictionary<string, bool>();
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

        
        protected string GameReturn(string _action,string _key,string[] _paras,string _culture = "zh-cn")
        {
            return JsonConvert.SerializeObject(new { action = _action, key = _key, paras = _paras, culture = _culture });

        }

        protected ContentResult ReturnGame(string _errorCode, string _errorMsg, object _result)
        {
            return Content(JsonConvert.SerializeObject(new { errorCode = _errorCode, errorMsg = _errorMsg, result = _result }));

        }

        protected static string GameReturnS(string _action, string _key, string[] _paras, string _culture = "zh-cn")
        {
            return JsonConvert.SerializeObject(new { action = _action, key = _key, paras = _paras, culture = _culture });

        }
        

        /// <summary>
        /// 功能:记录会员端的信息
        /// </summary>
        public string AN { get { return Session["AN"].ToString(); } }
        public string PWD { get { return Session["PWD"].ToString(); } }
        public string ID { get { return Session["id"].ToString(); } }
        public decimal Remainder { get { return decimal.Parse(Session["Remainder"].ToString()); } }


        /// <summary>
        /// 记录代理的信息
        /// </summary>
        public string Agent_ID { get { return Session["Agent_ID"].ToString(); } }
        public string Agent_Acc { get { return Session["Agent_Acc"].ToString(); } }

        //游戏相关
        public static string KEY = "c33e90a9-0714-48ee-89cc-8be9aff00710";
        public static string Integral;

        //单一登录的dic
        public static Dictionary<VipEntity, string> SSOVip = new Dictionary<VipEntity, string>();
        
        /// <summary>
        /// 在重写的Initialize方法(继承Controller的基类中)中不断的注册SessionId：
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Session["SessionId"] = Session.SessionID;
        }
        
        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        /// 
        public static string HttpPost(string reqUrl, string postData)
        {
            Debug.WriteLine("+++++++++++++++++++++++");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            HttpWebRequest request = null;
            try
            {
                if (reqUrl.IndexOf("?") == -1)
                {
                    reqUrl += "?trad=" + DateTime.Now.ToBinary().ToString();
                }
                else
                {
                    reqUrl += "&trad=" + DateTime.Now.ToBinary().ToString();
                }

                request = (HttpWebRequest)WebRequest.Create(reqUrl);
                request.Timeout = 20000;
                request.ReadWriteTimeout = 20000;
                request.ServicePoint.ConnectionLimit = int.MaxValue;
                request.Method = "POST";
                request.ContentType = "application/json; charset=UTF-8"; //"application/x-www-form-urlencoded";

                byte[] byteData = null;
                if (!string.IsNullOrEmpty(postData))
                {
                    byteData = UTF8Encoding.UTF8.GetBytes(postData);
                }

                if (byteData != null)
                {
                    request.ContentLength = byteData.Length;
                    using (Stream postStream = request.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string retText = reader.ReadToEnd();

                        sw.Stop();
                        return retText;
                    }
                }
            }
            catch (Exception ex)
            {
                if (sw.IsRunning)
                {
                    sw.Stop();
                }
                long elapsedTime = sw.ElapsedMilliseconds;
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }

                request = null;
            }

            return string.Empty;
        }
        
        /// <summary>
        /// 功能:刷新积分
        /// </summary>
        public void setMark()
        {
            string[] paras = { AN };

            string strKey = Md5.GetMd5(paras[0] + KEY);

            string param = GameReturn("GetCredit", strKey, paras);

            var result = HttpPost("http://172.16.31.232:9678/take", param);


            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("vip_AN", AN);
            decimal remainder = DALUtility.Vip.QryRemainder(dic);

            int x = result.LastIndexOf(":");
            string y = result.Substring(x);
            int z = y.IndexOf("}");
            Integral = y.Substring(1, z - 1) == "[]" ?"0": y.Substring(1, z - 1);
            Session["Remainder"] = remainder.ToString();
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

    public class result
    {
        public string ID { get; set; } = "";
        public string Name { get; set; } = "";
        public object Integral { get; set; }
    }

    public class result1
    {
        public string pageNum { get; set; } = "";
        public string total { get; set; } = "";
        public string pageSum { get; set; } = "";
        public List<object> data { get; set; }
    }

    public class result2
    {
        public string AccountName { get; set; }
        public object Integral { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
    }

}
