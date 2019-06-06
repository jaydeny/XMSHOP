using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Controllers;

namespace XM.WebAgent.Controllers
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-5/5
    /// 修改时间：2019-
    /// 功能：查询所有vip,修改vip信息等方法
    /// </summary>
    public class VIPController : BaseController
    {
        // GET: ManageVIP
        #region _vipInfo
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端获取所有的会员信息,可以分页
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult GetAll()
        {
            string sort = Request["sort"] == null ? "VipID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string userAn = Request["vip_AN"] == null ? "" : Request["vip_AN"];
            string userMp = Request["vip_mp"] == null ? "" : Request["vip_mp"];
            string userEmail = Request["vip_email"] == null ? "" : Request["vip_email"];
            int statusId = Request["status_id"] == null ? 1 : Convert.ToInt32(Request["status_id"]);
            string createDateTime = Request["vip_CDT"] == null ? "" : Request["vip_CDT"];
            int agentId = Request["agent_id"] == null ? 1 : Convert.ToInt32(Request["agent_id"]);


            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["vip_AN"] = userAn;
            paras["sort"] = sort;
            paras["order"] = order;
            paras["agent_AN"] = Session["Agent_AN"];
            var users = DALUtility.Vip.QryUsers<VipEntity>(paras, out totalCount);
            return PagerData(totalCount, users);
        }
        #endregion

        #region _UpdateVIP
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:会员端进入修改信息页面
        /// </summary>
        /// <returns>页面:修改信息页面</returns>
        public ActionResult Update()
        {
            return View();
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:会员端进行修改信息
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult Update(VipEntity vip)
        {
            return save(int.Parse(Request["ID"]));
        }
        #endregion

        #region _自定义

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/25
        /// 功能:添加/修改vip公用方法
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult save(int ID)
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = ID;
            paras["vip_AN"] = Request["vip_AN"];
            paras["vip_mp"] = Request["vip_mp"];
            paras["vip_Email"] = Request["vip_Email"];

            int iCheck = DALUtility.Vip.checkANandMBandEmail(paras);

            if (iCheck > 0)
            {
                return OperationReturn(false, iCheck == 1 ? "所输入的用户名重复,请重新输入!" : (iCheck == 2 ? "所输入的手机号码重复,请重新输入!" : "所输入的邮箱重复,请重新输入!"));
            }
            else
            {
                paras["vip_pwd"] = Request["vip_pwd"];
                paras["vip_CDT"] = DateTime.Now;
                paras["status_id"] = Request["status_id"] == null ? "1" : Request["status_id"];
                paras["agent_id"] = Request["agent_id"] == null ? "2" : Request["agent_id"];
                int result = DALUtility.Vip.saveVIP(paras);
                if (ID == 0)
                {
                    return OperationReturn(true, "注册成功");
                }
                else
                {
                    return OperationReturn(true, "修改成功");
                }
            }
        }
        #endregion
    }
}