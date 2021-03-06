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
using System.Web.SessionState;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : BaseController, IRequiresSessionState
    {
        #region 所有用户页面
        [PermissionFilter("User", "Index")]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region 修改密码页面
        public ActionResult PwdUpdate()
        {
            // 获取用户信息    添加人: 朱星宇，时间: 2019年5月10日13点48分
            UserEntity user = Session["User"] as UserEntity;
            ViewData["UserAccountName"] = user.UserAccountName;
            return View();
        }
        #endregion
        #region 修改密码操作
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="UserPwd"></param>
        /// <param name="NewPwd"></param>
        /// <returns></returns>
        public ActionResult UpdatePwd(string userPwd, string newPwd)
        {
            //string result = "";
            UserEntity uInfo = Session["User"] as UserEntity;
            UserEntity userChangePwd = new UserEntity();
            userChangePwd.id = uInfo.id;
            if (userPwd == uInfo.UserPassword)
            {
                userChangePwd.UserPassword = newPwd;
                if (DALUtility.User.ChangePwd(userChangePwd))
                {
                    return OperationReturn(true, "修改成功，请重新登录！");
                }
                else
                {
                    return OperationReturn(false, "修改失败！");
                }
            }
            else
            {
                return OperationReturn(false, "原密码不正确！");
            }
        }
        #endregion
        #region  获取所有用户信息
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [PermissionFilter("User", "Index")]

        public ActionResult GetAllUserInfo()
        {
            string sort = Request["order"] == null ? "id" : Request["order"];
            string order = Request["sort"] == null ? "asc" : Request["sort"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string userAn = Request["UserAccountName"] == null ? "" : Request["UserAccountName"];
            string userMp = Request["UserMobliePhone"] == null ? "" : Request["UserMobliePhone"];
            string userEmail = Request["UserEmail"] == null ? "" : Request["UserEmail"];
            int statusId = Request["StatusID"] == null ? 1 : Convert.ToInt32(Request["StatusID"]);
            int roleid = Request["RoleID "] == null ? 0 : Convert.ToInt32(Request["RoleID"]);



            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["userAn"] = userAn;
            paras["sort"] = sort;
            paras["order"] = order;
            if (roleid > 0)
            {
                paras["role_id"] = roleid;
            }
            var users = DALUtility.User.QryUsers<UserEntity>(paras, out totalCount);
            return PagerData(totalCount, users, pageindex, pagesize);
        }
        #endregion
        #region  添加/修改用户页面
        [PermissionFilter("User", "Index",Operationype.Add)]
        public ActionResult Form()
        {
            return View("_Form");
        }
        #endregion
        #region 添加或修改用户信息方法
        public ActionResult Save()
        {
            int id = Request["id"] == "" ? 0 : Convert.ToInt32(Request["id"]);
            string userid = Request["UserAccountName"];
            string mobilephone = Request["UserMobliePhone"];
            string email = Request["UserEmail"];
            int roleID = Convert.ToInt32(Request["RoleID"]);
            int statusID = Convert.ToInt32(Request["StatusID"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["user_AN"] = userid;
            paras["user_email"] = email;
            paras["status_id"] = statusID;
            paras["role_id"] = roleID;

            int iCheck = DALUtility.User.CheckUseridAndEmail(paras);
            if (iCheck > 0)
            {
                return OperationReturn(false, iCheck == 1 ? "用户名重复" : "邮箱重复");
            }
            else
            {
                int num;
                paras["user_mp"] = mobilephone;
                if (id == 0)
                {
                    paras["user_pwd"] = "xm123456";
                    paras["user_CBY"] = "admin";
                    paras["user_CDT"] = DateTime.Now;
                    num = DALUtility.User.Save(paras);
                    if (num > 0)
                    {
                        return OperationReturn(true, "添加成功！初始密码：" + paras["user_pwd"]);
                    }
                    else
                    {
                        return OperationReturn(false, "添加失败！");
                    }

                }
                num = DALUtility.User.Save(paras);
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
        #region  删除用户信息
        //[PermissionFilter("User", "Index", Operationype.Delete)]
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DelUserByIDs()
        {
            string Ids = Request["id"] == null ? "" : Request["id"];
            if (!string.IsNullOrEmpty(Ids))
            {
                return OperationReturn(DALUtility.User.DeleteUser(Ids), "删除成功");
            }
            else
            {
                return OperationReturn(false, "删除失败");
            }
        }
        #endregion
        #region 获取用户个人信息
        public ActionResult GetFormJson(string id)
        {
            var user = DALUtility.User.GetUserByUserId(id);
            return Content(JsonConvert.SerializeObject(user));
        }
        #endregion
        #region 获取当前用户个人信息页面
        public ActionResult UserInfo()
        {
            UserEntity user = Session["User"] as UserEntity;
            ViewData["id"] = user.id;
            ViewData["UserAccountName"] = user.UserAccountName;
            ViewData["UserMobliePhone"] = user.UserMobliePhone;
            ViewData["UserEmail"] = user.UserEmail;
            return View();
        }
        #endregion
        #region  获取当前用户
        public ActionResult InfoUser()
        {
            UserEntity user = Session["User"] as UserEntity;
            return GetFormJson(user.id.ToString());
        }
        #endregion
        #region 修改当前用户信息
        /// <summary>
        ///  修改当前用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdataInfo()
        {
            UserEntity user = Session["User"] as UserEntity;
            string email = Request["UserEmail"];
            bool boo = DALUtility.User.JudgeEmail(user.id, email);
            if (boo)
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras["id"] = user.id;
                paras["user_email"] = email;
                paras["user_mp"] = Request["UserMobliePhone"];
                int num = DALUtility.User.Save(paras);
                if (num > 0)
                {
                    return OperationReturn(true, "修改成功！");
                }
                else
                {
                    return OperationReturn(false, "修改失败！");
                }
            }
            else
            {
                return OperationReturn(false, "邮箱重复");
            }
        }
        #endregion
    }
}