using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XM.Comm;
using XM.Model;
using XM.Web.Domain;

namespace XM.Web.Controllers
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 首页
    /// </summary>
    public class HomeController : BaseController
    {
        #region  主页面
        // GET: Home
        public ActionResult Index()
        {
            UserEntity uInfo = Session["User"] as UserEntity;
            if (uInfo == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.RealName = uInfo.UserAccountName;
            ViewBag.Title = "首页";
            return View();
        }
        #endregion
        #region  加载菜单方法
        public ActionResult LoadMenu()
        {
            IEnumerable<Navbar> objRoleMenu = (IEnumerable<Navbar>)Session["RoleMenu"];
            List<int> objIDs = new List<int>();
            foreach(Navbar roleMenu in objRoleMenu)
            {
                objIDs.Add(roleMenu.MenuId);
            }
            List<MenuEntity> objMenus = DALUtility.Menu.GetAllMenuByIds(objIDs);
            return PagerData(objMenus.Count,objMenus);
        }
        #endregion
        public ActionResult GetCommonData()
        {
            CommonDataDTO common = new CommonDataDTO();
            common.Roles = DALUtility.Role.QryRole<RoleEntity>();
            common.Types = DALUtility.Dic.GetDicByTag(15).ToList();
            common.Menus = DALUtility.Menu.QryAllMenu<MenuEntity>();
            int roleId = Convert.ToInt32(Session["RoleID"]);
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("r_id", roleId);
            var roleMenus = DALUtility.RoleMenu.QryRoleMenu<Navbar>(paras);
            Session["RoleMenu"] = roleMenus;
            common.Navbars = (IEnumerable<Navbar>)Session["RoleMenu"];
            common.Agents = DALUtility.Agent.QryAgent<AgentEntity>();
            return Content(JsonConvert.SerializeObject(common));
        }

        public ActionResult Default()
        {
            return View();
        }
        public ActionResult GetDefault()
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("year", "2019");
            paras.Add("startMonth", Request["startMonth"] == null ? "" : Request["startMonth"]);
            paras.Add("endMonth", Request["endMonth"] == null ? "" : Request["endMonth"]);
            paras.Add("agent_AN", Request["agent_AN"] == null ? "" : Request["agent_AN"]);
            return Content(JsonConvert.SerializeObject(DALUtility.First.GetStore<MonthEntity>(paras)));
        }
        public ActionResult GetBar()
        {
            string action = "GetRecordCollectByAllAgency";

            string starttime = Request["starttime"] == null ? "2019-04" : Request["starttime"];
            string endtime = Request["endtime"] == null ? "2019-06" : Request["endtime"];

            string[] paras = { starttime, endtime };
            string key = Md5.GetMd5(paras[0] + paras[1]  + KEY);
            string param = GameReturn(action, key, paras);
            var result = HttpPost(param);
            return Content(result);
            
        }
    }
}