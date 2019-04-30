using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;
using System.Web.SessionState;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/4/22
    /// 用户管理控制器
    /// </summary>
    public class UserController : BaseController, IRequiresSessionState
    {
        //[PermissionFilter]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PwdUpdate()
        {
            return View();
        }
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="UserPwd"></param>
        /// <param name="NewPwd"></param>
        /// <param name="ConfirmPwd"></param>
        /// <returns></returns>
        public ActionResult UpdatePwd(string UserPwd, string NewPwd, string ConfirmPwd)
        {
            //string result = "";
            UserEntity uInfo = Session["User"] as UserEntity;

            UserEntity userChangePwd = new UserEntity();
            userChangePwd.id = uInfo.id;
            userChangePwd.UserPassword = NewPwd;

            if (UserPwd == uInfo.UserPassword)
            {
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
            //return Content(result);
        }
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        //[PermissionFilter("User", "Index")]

        public ActionResult GetAllUserInfo()
        {
            string sort = Request["sort"] == null ? "id" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string userAn = Request["user_AN"] == null ? "" : Request["user_AN"];
            string userMp = Request["user_mp"] == null ? "" : Request["user_mp"];
            string userEmail = Request["user_email"] == null ? "" : Request["user_email"];
            int statusId = Request["status_id"] == null ? 1 : Convert.ToInt32(Request["status_id"]);
            int roleid = Request["role_id "] == null ? 0 : Convert.ToInt32(Request["role_id"]);



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
            return PagerData(totalCount, users);
        }
        public ActionResult UserAdd()
        {
            return View("_UserAdd");
        }

        /// <summary>
        /// 新增 用户
        /// </summary>
        /// <returns></returns>
        [PermissionFilter("User", "Index", Operationype.Add)]
        public ActionResult AddUser()
        {
            return SaveUser();

        }

        public ActionResult UserEdit()
        {
            return View("_UserEdit");
        }
        /// <summary>
        /// 编辑 用户
        /// </summary>
        /// <returns></returns>
        [PermissionFilter("User", "Index", Operationype.Update)]
        public ActionResult EditUser()
        {

            return SaveUser();
        }
        /// <summary>
        /// 添加或修改用户信息方法
        /// </summary>
        /// <returns></returns>
        private ActionResult SaveUser()
        {
            int id = Convert.ToInt32(Request["id"]);
            string userid = Request["user_AN"];
            string mobilephone = Request["user_mp"];
            string email = Request["user_email"];
            int roleID = Convert.ToInt32(Request["role_id"]);
            int statusID = Convert.ToInt32(Request["status_id"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = id;
            paras["user_AN"] = userid;
            paras["user_email"] = email;

            int iCheck = DALUtility.User.CheckUseridAndEmail(paras);
            if (iCheck > 0)
            {
                return OperationReturn(false, iCheck == 1 ? "用户名重复" : "邮箱重复");
            }
            else
            {
                int num;
                paras["role_id"] = roleID;
                paras["user_mp"] = mobilephone;
                paras["status_id"] = statusID;
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
    }
}