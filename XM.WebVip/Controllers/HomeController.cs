using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Comm;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-5/5
    /// 修改时间：2019-
    /// 功能：登录,注册,修改,安全退出等方法
    /// </summary>
    public class HomeController : BaseController
    {

       
        // GET: Home
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:返回会员端首页
        /// </summary>
        /// <returns>页面:首页</returns>
        public ActionResult Index()
        {
            ViewData["VipAccountName"] = Session["AN"];
            return View();
        }

        #region _Login
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:返回会员端登录页面
        /// </summary>
        /// <returns>页面:登录页面</returns>
        public ActionResult Login()
        {
            return View("_Login");
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:会员端进行登入的方法
        /// </summary>
        /// <returns>json值</returns>
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


                   
                    Session["AN"] = vip.VipAccountName;
                    Session["ID"] = vip.VipID;
                    Session["PWD"] = vip.VipPassword;
                    Session["Remainder"] = getRemainder(vip.VipAccountName);


                    Session["Agent_ID"] = vip.AgentID;
                    Session["Agent_Acc"] = getAgentAN(vip.AgentID);
                    //把购物车项添加到数据库
                    addCart();

                    //base.Agent_Acc = agent_an;
                    return OperationReturn(true, "登录成功,vip_id:" + vip.VipID + ";vip_AN:" + AN,
                        new
                        {
                            vip_id = vip.VipID,
                            vip_AN = vip.VipAccountName,
                            agent_id = vip.AgentID
                        });

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
        #endregion

        #region _Signin
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:返回会员端注册页面
        /// </summary>
        /// <returns>页面:注册时,返回注册页面</returns>
        public ActionResult Signin()
        {
            return View("_Signin");
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:会员端进行注册
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult Signin(VipEntity vip)
        {
            return save(0);
        }
        #endregion

        #region _update
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:会员端进入修改信息页面
        /// </summary>
        /// <returns>页面:修改信息页面</returns>
        public ActionResult Update()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_AN", Session["AN"].ToString());

            var result = DALUtility.Vip.QryVipInfo<VipInfoDTO>(param);

            ViewData["VipAccountName"] = Session["AN"];
            ViewData["Email"] = result.VipEmail;
            ViewData["MP"] = result.VipMobliePhone;
            return View("_Update");
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
            return save(int.Parse(Session["ID"].ToString()));
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/28
        /// 功能:返回密码找回页面,输入用户名  
        /// 进入修改密码页面  
        /// </summary>
        /// <returns>页面</returns>
        public ActionResult FoundPwdPage()
        {
            return View("_FoundPwdPage");
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/28
        /// 功能:发送邮件
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult FoundPwd()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_AN", Request["vip_AN"]);
            var vip = DALUtility.Vip.QryVipEmail<VipEntity>(param);

            bool boo = false;
            string strMailContent = "<a href=\"http://172.16.31.234:6666/VIP/UpdatePwdPage&" + vip.VipID + "\">修改密码</a>";

            if (vip != null)
            {
                boo = EmailHelper.send(vip.VipEmail, "修改密码", strMailContent);
            }

            return OperationReturn(boo, "邮件已发送,请登录邮箱进行下一步操作!");
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/28
        /// 功能:进入找回密码页面
        /// </summary>
        /// <returns>页面</returns>
        public ActionResult PwdFoundPage()
        {
            return View("_PwdFoundPage");

        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/28
        /// 功能:根据id找回密码
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult PwdFound()
        {
            return save(int.Parse(Request["vip_id"]));
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/28
        /// 功能:进入修改密码页面
        /// </summary>
        /// <returns>页面</returns>
        public ActionResult UpdatePwdPage()
        {
            return View("_UpdatePwdPage");
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/28
        /// 功能:根据id修改密码
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult UpdatePwd()
        {
            int vip_id = int.Parse(Session["ID"].ToString());

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_id", vip_id);

            string strOrgPwd = DALUtility.Vip.QryOrgPwd(param);

            string strOriginalPwd = Request["oldPwd"];

            if (strOrgPwd.Equals(strOriginalPwd))
            {

                return save(vip_id);
            }
            else
            {
                return OperationReturn(false, "修改失败,原始密码出错,请重新输入!");
            }
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
            paras["vip_AN"] = Session["AN"] != null ? Session["AN"].ToString() : Request["vip_AN"];
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
                paras["agent_id"] = Request["agent_id"] == null ? "1" : Request["agent_id"];
                int result = DALUtility.Vip.saveVIP(paras);
                if (ID == 0)
                {
                    NewVIP(paras["vip_AN"].ToString());
                    return OperationReturn(true, "注册成功");
                }
                else
                {
                    return OperationReturn(true, "修改成功");
                }
            }
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4-28
        /// 修改时间：2019-
        /// 功能：获取代理商AN
        /// </summary>
        public string getAgentAN(int id)
        {
            Dictionary<string, object> Agent_id = new Dictionary<string, object>();
            Agent_id.Add("agent_id", id);
            
            string result = DALUtility.Vip.QryAgentANByID(Agent_id);
            return result;
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4-28
        /// 修改时间：2019-
        /// 功能：获取代理商AN
        /// </summary>
        public decimal getRemainder(string AN)
        {
            Dictionary<string, object> Remainder = new Dictionary<string, object>();
            Remainder.Add("vip_AN", AN);
            decimal result = DALUtility.Vip.QryRemainder(Remainder);
            return result;
        }


        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4-28
        /// 修改时间：2019-
        /// 功能：安全退出
        /// </summary>
        public ActionResult RemoveSession() 
        {
            Session.Remove("AN");
            Session.Remove("ID");
            Session.Remove("Agent_ID");
            Session.Remove("Agent_Acc");
           
            return OperationReturn(true, "退出成功");
        }

        /// <summary>
        /// 功能:新用户添加20积分
        /// </summary>
        /// <param name="vip_AN"></param>
        /// <returns></returns>
        public int NewVIP(string vip_AN)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("vip_AN", vip_AN);
            return DALUtility.Vip.NweVIP(dic);
        }

        /// <summary>
        /// 将存储在HashTable里的数据添加到数据库
        /// </summary>
        public void addCart() {
            if (cartTable.Count > 0)
            {
                foreach (ShoppCartEntity item in cartTable.Values)
                {
                    Dictionary<string, object> paras = new Dictionary<string, object>();
                    paras["editType"] = 1;
                    paras["itemID"] = 0;
                    paras["vipID"] = Convert.ToInt32(ID);
                    paras["AgoodsID"] = item.Agoods_ID;
                    paras["count"] = item.Agoods_Count;
                    int res = DALUtility.ShoppCart.EditCart(paras);
                }
            }
            cartTable.Clear();
        } 
        
        #endregion
    }
}