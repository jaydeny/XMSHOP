﻿using FrameWork.MongoDB;
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
