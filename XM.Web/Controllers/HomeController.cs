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
    public class HomeController : BaseController
    {
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
        public ActionResult LoadMenu()
        {
            List<RoleMenuEntity> roleMenus = new List<RoleMenuEntity>();
            string strRoleMenuData = Session["RoleMenu"].ToString();
            List<int> objIDs = new List<int>();
            foreach(int objId in objIDs)
            {
                objIDs.Add(JsonConvert.DeserializeObject<RoleMenuEntity>(strRoleMenuData).MenuId);
            }
                
            var menu = DALUtility.Menu.GetAllMenuById(objIDs);
            return PagerData(9, menu);

        }
    }
}