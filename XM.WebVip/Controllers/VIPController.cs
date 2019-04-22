using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XM.Model;
using XM.Web.Controllers;

namespace XM.WebVip.Controllers
{
    public class VIPController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string AN, string pwd)
        {
            try
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras["vip_AN"] = AN;
                paras["vip_pwd"] = pwd;
                //paras["vip_pwd"] = Md5.GetMD5String(pwd);   //md5加密

                var vip = DALUtility.Vip.QryVipToLogin<VipEntity>(paras);
                if (vip != null)
                {
                    if (vip.StatusID == 2)
                    {
                        return OperationReturn(false, "用户已被禁用，请您联系管理员");
                    }
                    return OperationReturn(true, "登录成功,vip_id:"+vip.VipID+";vip_AN:"+vip.VipAccountName);
                }
                else
                {
                    return OperationReturn(false, "用户名密码错误，请您检查");
                }
            }
            catch (Exception ex)
            {
                return OperationReturn(false, "登录异常," + ex.Message);
            }
        }

        //注册时,返回注册页面
        public ActionResult Signin()
        {
            return View();
        }

        //注册
        [HttpPost]
        public ActionResult Signin(VipEntity vip)
        {
            return save(0);
        }

        //修改会员
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(VipEntity vip)
        {
            return save(vip.VipID);
        }

        //注册或者修改会员信息时,检查邮箱,email,联系方式舒服重复
        public ActionResult save(int ID)
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = Request["vip_id"];
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
                paras["status_id"] = Request["status_id"];
                paras["agent_id"] = Request["agent_id"];
                int result = DALUtility.Vip.saveVIP(paras);
                return OperationReturn(result > 0);
            }
        }

        public ActionResult Invitation(string vip_AN)
        {
            return OperationReturn(true, vip_AN);
        }

        //获取所有的vip
        public ActionResult GetAllVIP()
        {
            string sort = Request["sort"] == null ? "id" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            string vip_AN = Request["vip_AN"];
            string vip_mp = Request["vip_mp"];
            string vip_Email = Request["vip_Email"];
            string status_id = Request["status_id"];
            string agent_id = Request["agent_id"];

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("vip_AN", vip_AN);
            param.Add("vip_mp", vip_mp);
            param.Add("vip_Email", vip_Email);
            param.Add("status_id", status_id);
            param.Add("agent_id", agent_id);


            string result = DALUtility.Vip.QryAllVIP(param, out int ICount);
            return Content(result);
        }


        //未完成,需要代理端同意
        public ActionResult Recharge()
        {
            DateTime date = DateTime.Now;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("recharge_name", Request["recharge_name"]);
            param.Add("recharge_price", Request["recharge_price"]);
            param.Add("recharge_time", date);
            param.Add("agent_id", Request["agent_id"]);
            param.Add("vip_id", Request["vip_id"]);

            int iCheck = DALUtility.Agent.Recharge(param);

            if(iCheck > 0)
            {
                Url.Action("CheckRecharge","Agent",new RouteValueDictionary {
                    { "vip_id",Request["vip_id"]},
                    { "recharge_price",Request["recharge_price"]},
                    { "recharge_time", date}
                });

                return OperationReturn(true, "等待中");
            }
            return OperationReturn(false,"充值失败");
        }
    }
}