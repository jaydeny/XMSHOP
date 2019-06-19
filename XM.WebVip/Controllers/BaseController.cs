/*-------------------------------------*
 * 创建人:         曾贤鑫
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       曾贤鑫       创建
 *-------------------------------------*/
using Newtonsoft.Json;
using System.Collections;
using System.Web.Mvc;
using XM.DALFactory;

namespace XM.WebVIP.Controllers
{
    /// <summary>
    /// 基本类
    /// </summary>
    public class BaseController : Controller
    {

        #region 购物车
        /// <summary>
        /// 购物车项
        /// </summary>
        public static Hashtable cartTable = new Hashtable();
        #endregion

        #region 数据交互接口
        /// <summary>
        /// 数据交互接口
        /// </summary>
        internal DALCore DALUtility => DALCore.GetInstance();
        #endregion

        #region 数据转化展示
        /// <summary>
        /// 数据展示
        /// </summary>
        /// <param name="totalCount">数据条数</param>
        /// <param name="rows">数据体</param>
        /// <returns></returns>
        protected ContentResult PagerData(int totalCount, object rows)
        {
            return Content(JsonConvert.SerializeObject(new { total = totalCount.ToString(), rows = rows }));
        }
        /// <summary>
        /// 数据转化格式展示
        /// </summary>
        /// <param name="_success">是否成功</param>
        /// <param name="_msg">返回信息</param>
        /// <returns></returns>
        protected ContentResult OperationReturn(bool _success, string _msg = "")
        {
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success }));

        }
        /// <summary>
        /// 数据转化格式展示
        /// </summary>
        /// <param name="_success">是否成功</param>
        /// <param name="_msg">返回信息</param>
        /// <param name="obj">json序列化对象</param>
        /// <returns></returns>
        protected ContentResult OperationReturn(bool _success, string _msg = "", object obj = null)
        {
            return Content(JsonConvert.SerializeObject(new { msg = _msg != "" ? _msg : (_success ? "操作成功" : "操作失败"), success = _success, data = obj }));

        }
        /// <summary>
        /// 游戏数据返回格式
        /// </summary>
        /// <param name="_action"></param>
        /// <param name="_key"></param>
        /// <param name="_paras"></param>
        /// <param name="_culture"></param>
        /// <returns></returns>
        protected string GameReturn(string _action,string _key,string[] _paras,string _culture = "zh-cn")
        {
            return JsonConvert.SerializeObject(new { action = _action, key = _key, paras = _paras, culture = _culture });

        }
        /// <summary>
        /// 游戏数据返回格式
        /// </summary>
        /// <param name="_action"></param>
        /// <param name="_key"></param>
        /// <param name="_paras"></param>
        /// <param name="_culture"></param>
        /// <returns></returns>
        protected static string GameReturnS(string _action, string _key, string[] _paras, string _culture = "zh-cn")
        {
            return JsonConvert.SerializeObject(new { action = _action, key = _key, paras = _paras, culture = _culture });

        }
        #endregion

        #region 会员信息
        /// <summary>
        /// 功能:记录会员端的信息
        /// </summary>
        public string AN { get { return Session["AN"].ToString(); } }
        /// <summary>
        /// 密码
        /// </summary>
        public string PWD { get { return Session["PWD"].ToString(); } }
       /// <summary>
       /// 用户ID
       /// </summary>
        public string ID { get { return Session["id"].ToString(); } }
        /// <summary>
        /// 用户余额
        /// </summary>
        public decimal Remainder { get { return decimal.Parse(Session["Remainder"].ToString()); } }
        /// <summary>
        /// 用户积分
        /// </summary>
        public static string Integral;

        /// <summary>
        /// 代理ID
        /// </summary>
        public string Agent_ID { get { return Session["Agent_ID"].ToString(); } }
        /// <summary>
        /// 会员对应的代理账号
        /// </summary>
        public string Agent_Acc { get { return Session["Agent_Acc"].ToString(); } }
        #endregion

    }
}
