using FrameWork.MongoDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalCount">总记录数</param>
        /// <param name="rows">数据</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">页面条数</param>
        /// <returns></returns>
        protected ContentResult PagerData(int totalCount, object rows, int page, int pageSize)
        {
            // return Content(JsonConvert.SerializeObject(new { total = totalCount.ToString(), rows = rows }));
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

        protected ContentResult OperationReturn(bool _success, string _msg = "")
        {
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success }));

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
