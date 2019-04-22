using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Model;

namespace XM.Web.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
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
            UserEntity uInfo = ViewData["Account"] as UserEntity;

            UserEntity userChangePwd = new UserEntity();
            userChangePwd.UserID = uInfo.UserID;
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

        public ActionResult GetAllUserInfo()
        {
            string sort = Request["sort"] == null ? "ID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string userAn = Request["user_an"] == null ? "" : Request["user_an"];
            string userMp = Request["user_mp"] == null ? "" : Request["user_mp"];
            string userEmail = Request["user_email"] == null ? "" : Request["user_email"];
            string statusId = Request["status_id"] == null ? "" : Request["status_id"];



            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["userAn"] = userAn;
            paras["sort"] = sort;
            paras["order"] = order;
            //if (roleid > 0)
            //{
            //    paras["RoleID"] = roleid;
            //}
            var users = DALUtility.User.QryUsers<UserEntity>(paras, out totalCount);
            return PagerData(totalCount, users);
        }


        /// <summary>
        /// 新增 用户
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
        {
            return SaveUser();

        }


        /// <summary>
        /// 编辑 用户
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUser()
        {

            return SaveUser();
        }

        private ActionResult SaveUser()
        {
            int id = Convert.ToInt32(Request["id"]);
            string userid = Request["UserID"];
            string mobilephone = Request["MobilePhone"];
            string email = Request["Email"];
            int roleID = Convert.ToInt32(Request["roleId"]);
            int statusID = Convert.ToInt32(Request["statusId"]);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["ID"] = id;
            paras["AccountName"] = userid;
            paras["RoleID"] = roleID;
            paras["MobilePhone"] = mobilephone;
            paras["Email"] = email;

            if (id == 0)
            {
                paras["Password"] = "xm123456";
                paras["CreateBy"] = "admin";
                paras["CreateTime"] = DateTime.Now;
            }
            int iCheck = DALUtility.User.CheckUseridAndEmail(paras);
            if (iCheck > 0)
            {
                return OperationReturn(false, iCheck == 1 ? "用户名重复" : "邮箱重复");
            }
            else
            {
                return OperationReturn(DALUtility.User.Save(paras) > 0);
            }
        }

        public ActionResult DelUserByIDs()
        {
            string Ids = Request["IDs"] == null ? "" : Request["IDs"];
            if (!string.IsNullOrEmpty(Ids))
            {
                return OperationReturn(DALUtility.User.DeleteUser(Ids));
            }
            else
            {
                return OperationReturn(false);
            }
        }



        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        //public ActionResult GetAllRoleInfo()
        //{
        //    string roleJson = JsonHelper.ToJson(DALUtility.Role.GetAllRole("1=1"));
        //    return Content(roleJson);
        //}

    }
}