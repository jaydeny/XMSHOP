﻿/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XM.DALFactory;
using XM.Model;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 中间层
    /// </summary>
    public class BaseController : Controller
    {
        #region Session属性

        //这两个字段有可能赋予失败,引用实例失败,数据存入session可以成功,但是存入字段会失败
        /// <summary>
        /// Session对象中的代理名
        /// </summary>
        public string Agent_AN { get { return Session["Agent_AN"].ToString(); } }
        /// <summary>
        /// Session对象中的ID
        /// </summary>
        public string Agent_ID { get { return Session["Agent_ID"].ToString(); } }
        #endregion

        #region SSO
        /// <summary>
        /// 代理SSO集合
        /// </summary>
        public static Dictionary<AgentEntity, string> SSOAgent = new Dictionary<AgentEntity, string>();
        #endregion

        #region 数据交互接口
        /// <summary>
        /// 数据交互接口
        /// </summary>
        internal DALCore DALUtility => DALCore.GetInstance();
        #endregion

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

        #region 数据返回格式
        /// <summary>
        /// 数据返回格式: 数据数 , 数据
        /// </summary>
        /// <param name="totalCount">总条数</param>
        /// <param name="rows">数据体对象</param>
        /// <returns>json格式字符串</returns>
        protected ContentResult PagerData(int totalCount, object rows)
        {
            return Content(JsonConvert.SerializeObject(new { total = totalCount.ToString(), rows = rows }));
        }
        /// <summary>
        /// 数据返回格式 : bool + 成功信息
        /// </summary>
        /// <param name="_success">是否成功</param>
        /// <param name="_msg">成功信息</param>
        /// <returns>json格式字符串</returns>
        protected ContentResult OperationReturn(bool _success, string _msg = "")
        {
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success }));

        }
        /// <summary>
        /// 数据返回格式 : bool + 成功信息 + 数据
        /// </summary>
        /// <param name="_success">是否成功</param>
        /// <param name="_msg">成功信息</param>
        /// <param name="obj">数据体对象</param>
        /// <returns>json格式字符串</returns>
        protected ContentResult OperationReturn(bool _success, string _msg = "", object obj = null)
        {
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success, data = obj }));

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
}
