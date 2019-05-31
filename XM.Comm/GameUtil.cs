using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using FrameWork.Extension;
namespace XM.Comm
{
    /// <summary>
    /// 作者:梁钧淋
    /// 创建时间:2019/5/28
    /// 功能: 接入游戏端功能类
    /// </summary>
    public class GameUtil
    {
        //连接键
        public readonly static string KEY = "GameKey".ValueOfAppSetting();
        //游戏端API接口URL
        private readonly static string GameUrl = "GameUrl".ValueOfAppSetting();


        /// <summary>
        /// 定义接入GameAPI数据转换方法
        /// </summary>
        /// <param name="_action">方法名</param>
        /// <param name="_key">通信key</param>
        /// <param name="_paras">参数数组</param>
        /// <param name="_culture">国际化参数</param>
        /// <returns>游戏json数据</returns>
        protected string GameReturn(string _action, string _key, string[] _paras, string _culture = "zh-cn")
        {
            return JsonConvert.SerializeObject(new { action = _action, key = _key, paras = _paras, culture = _culture });
        }

        /// <summary>
        /// 连接游戏端，并发送参数
        /// </summary>
        /// <param name="reqUrl">游戏端接口URL</param>
        /// <param name="postData">加密成string的请求参数</param>
        /// <returns>游戏端产出的json字符串</returns>
        public string HttpPost(string reqUrl , string postData)
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

        /// <summary>
        /// 连接游戏端，并发送参数
        /// </summary>
        /// <param name="reqUrl">游戏端接口URL</param>
        /// <param name="postData">加密成string的请求参数</param>
        /// <returns>游戏端产出的json字符串</returns>
        public static string HttpPost(string postData)
        {
            string reqUrl = GameUrl;
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
        /// 收集参数，发送请求游戏后端并得到json格式字符串响应
        /// </summary>
        /// <param name="strs">参数数组</param>
        /// <param name="action">接口方法名</param>
        /// <returns>游戏端产出的json字符串</returns>
        public string ReturnRes(string[] strs, string action)
        {
            int len = strs.Length;
            var sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb.Append(strs[i]);
            }

            string strKey = Md5.GetMd5(sb.ToString() + KEY);
            string paras = GameReturn(action, strKey, strs);

            return HttpPost(GameUrl, paras);
        }
        
    }
}

