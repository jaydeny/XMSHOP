using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
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

        #region  分页方法（常用）
        /// <param name="totalCount">总记录数</param>
        /// <param name="rows">数据</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">页面条数</param>
        /// <returns></returns>
        protected ContentResult PagerData(int totalCount, object rows, int page, int pageSize)
        {
            var data = new
            {
                // 数据
                rows = rows,
                // 总页数
                total = (int)Math.Ceiling((double)totalCount / pageSize),
                // 当前页
                page = page,
                // 总记录数
                records = totalCount
            };
            return Content(JsonConvert.SerializeObject(data));
        }
        #endregion

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
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success, data = obj }));

        }
        //定义接入GameAPI转换方法
        protected string GameReturn(string _action, string _key, string[] _paras, string _culture = "zh-cn")
        {
            return JsonConvert.SerializeObject(new { action = _action, key = _key, paras = _paras, culture = _culture });

        }
        public string Agent_AN { get { return Session["AN"].ToString(); } }
        public string Agent_ID { get { return Session["id"].ToString(); } }


        public static Dictionary<AgentEntity, string> SSOAgent = new Dictionary<AgentEntity, string>();
        #region 游戏专用
        //连接键
        public static string KEY = "c33e90a9-0714-48ee-89cc-8be9aff00710";
        //游戏端API接口
        public static string GameUrl = "http://172.16.31.232:9678/take";
        ////封装游戏端请求API
        public static string HttpPost(string reqUrl, string postData)
        {
           
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
        //数据加密转换
        public string ReturnRes(string[] strs ,string action) {

            int len = strs.Length;
            var sb = new  StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb.Append(strs[i]);
            }

            string strKey = Md5.GetMd5(sb.ToString() +  KEY);
            string paras = GameReturn(action, strKey, strs);

            return HttpPost(GameUrl, paras);
        }

    }
    #endregion

    #region  返回结果
    /// <summary>
    /// 返回结果
    /// </summary>
    public class result_base
    {
        public string errorCode { get; set; } = "";
        public string errorMsg { get; set; } = "";
        public object result { get; set; }
    }
    #endregion
}
