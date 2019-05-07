using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/4/22
    /// VIP管理
    /// </summary>
    public class VipController : BaseController
    {
        #region 所有VIP页面
        //[PermissionFilter]
        // GET: Vip
        public ActionResult Index()
        {

            return View();
        }
        #endregion
        #region 获取所有vip信息
        //[PermissionFilter("Vip","Index")]
        public ActionResult GetAllUserInfo()
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
            var users = DALUtility.Vip.QryUsers<VipEntity>(paras, out totalCount);
            return PagerData(totalCount, users);
        }
        #endregion
        #region  添加/修改页面
        public ActionResult Form()
        {
            return View("_Form");
        }
        #endregion
        #region  添加/修改操作
        public ActionResult Save()
        {
            int id = Convert.ToInt32(Request["id"]);
            string userid = Request["vip_AN"];
            string mobilephone = Request["vip_mp"];
            string email = Request["vip_email"];
            int statusID = Convert.ToInt32(Request["status_id"]);
            int agentId = Convert.ToInt32(Request["agent_id"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["vip_AN"] = userid;
            paras["vip_mp"] = mobilephone;
            paras["vip_email"] = email;


            int iCheck = DALUtility.Vip.CheckUseridAndEmail(paras);
            if (iCheck > 0)
            {
                return OperationReturn(false, iCheck == 1 ? "用户名重复" : "邮箱重复");
            }
            else
            {
                int num;
                paras["status_id"] = statusID;
                paras["agent_id"] = agentId;
                if (id == 0)
                {
                    paras["vip_pwd"] = "xm123456";
                    paras["vip_CDT"] = DateTime.Now;
                    num = DALUtility.Vip.Save(paras);
                    if (num > 0)
                    {
                        return OperationReturn(true, "添加成功！初始密码：" + paras["vip_pwd"]);
                    }
                    else
                    {
                        return OperationReturn(false, "添加失败");
                    }
                    
                }
                num = DALUtility.Vip.Save(paras);
                if (num > 0)
                {
                    return OperationReturn(true, "修改成功！");
                }
                else
                {
                    return OperationReturn(false, "修改失败！");
                }
                
            }
        }
        #endregion
        #region 删除操作
        //[PermissionFilter("Vip", "Index", Operationype.Delete)]
        public ActionResult DelUserByIDs()
        {
            string Ids = Request["id"] == null ? "" : Request["id"];
            if (!string.IsNullOrEmpty(Ids))
            {
                return OperationReturn(DALUtility.Vip.DeleteUser(Ids),"删除成功");
            }
            else
            {
                return OperationReturn(false,"删除失败");
            }
        }
        #endregion
        #region  获取VIP个人信息
        public ActionResult GetFormJson(string id)
        {
            var vip = DALUtility.Vip.GetUserByUserId(id);
            return Content(JsonConvert.SerializeObject(vip));
        }
        #endregion
    }
}