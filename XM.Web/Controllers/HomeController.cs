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
            IEnumerable<RoleMenuEntity> objRoleMenu = (IEnumerable<RoleMenuEntity>)Session["RoleMenu"];
            List<int> objIDs = new List<int>();
            foreach(RoleMenuEntity roleMenu in objRoleMenu)
            {
                objIDs.Add(roleMenu.MenuId);
            }
            List<MenuEntity> objMenus = DALUtility.Menu.GetAllMenuByIds(objIDs);
            return PagerData(2,objMenus);
        }
    }
}