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
    /// 作者:曾贤鑫
    /// 日期:2019/4/26
    /// 功能:代理端需要用到的一些行为
    /// </summary>
    public class AgentController : BaseController
    {
        // GET: Agent
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:返回代理端首页
        /// </summary>
        /// <returns>页面:代理端首页</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 作者:梁钧淋
        /// 日期:2019/4/26
        /// 功能:返回商品页面
        /// </summary>
        /// <returns></returns>
        //返回商品操作页
        public ActionResult getGoodsPage()
        {
            return View();
        }
        /// <summary>
        /// 作者:梁钧淋
        /// 日期:2019/4/29
        /// 功能:返回报表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportForm() {
            return View();
        }
        /// <summary>
        /// 作者:梁钧淋
        /// 日期:2019/4/30
        /// 功能:返回代理个人信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AgentPersonalCenter()
        {
            return View();
        }




        #region _Login
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:返回代理端登录页
        /// </summary>
        /// <returns>页面:代理端登录页</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行登入
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult Login(string AN, string pwd)
        {
            try
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras["agent_AN"] = AN;
                paras["agent_pwd"] = pwd;

                var agent = DALUtility.Agent.QryAgentToLogin<AgentEntity>(paras);
                if (agent != null)
                {
                    if (agent.StatusID == 2)
                    {
                        return OperationReturn(false, "用户已被禁用，请您联系管理员");
                    }
                    Session["Agent_AN"] = agent.AgentAccountName;
                    Session["Agent_ID"] = agent.AgentID;
                    return OperationReturn(true, "登录成功,agent_id:" + agent.AgentID + ";agent_AN:" + AN,
                        new
                        {
                            agent_id = agent.AgentID,
                            agent_AN = agent.AgentAccountName,
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
        /// 功能:返回代理端注册页
        /// </summary>
        /// <returns>页面:代理端注册页</returns>
        public ActionResult Signin()
        {
            return View();
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行注册
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult Signin(AgentEntity agent)
        {
            return save(0);
        }
        #endregion

        #region _Update
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行修改信息
        /// </summary>
        /// <returns>页面:修改页面</returns>
        public ActionResult Update()
        {
            return View();
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行修改信息
        /// </summary>
        /// <returns>json值</returns>
        [HttpPost]
        public ActionResult Update(AgentEntity agent)
        {
            return save(int.Parse(Session["Agent_ID"].ToString()));
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:会员端进入修改信息页面
        /// </summary>
        /// <returns>页面:修改信息页面</returns>
        public ActionResult UpdateVIP()
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
        public ActionResult UpdateVIP(VipEntity vip)
        {
            return saveVIP(int.Parse(Request["ID"]));
        }
        #endregion

        #region _vipInfo
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端获取所有的会员信息,可以分页
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult GetAllVIP()
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

        #region _AgentInfo
        public ActionResult QryAgentInfo()
        {
            if(Session["Agent_AN"] != null)
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras.Add("agent_AN", Session["Agent_AN"].ToString());

                return Content(DALUtility.Agent.QryAgentInfo<AgentEntity>(paras));
            }
            else
            {
                return OperationReturn(false,"请登录后查看个人信息");
            }
        }
        #endregion

        #region _Goods
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行商品上架或者修改
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult MakeGoods()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("id", Request["Agoods_id"]);
            param.Add("goods_id", Request["goods_id"]);
            param.Add("status_id", Request["status_id"]);
            param.Add("price", Request["price"]);
            param.Add("up_time", DateTime.Now);
            param.Add("Agent_AN", Session["Agent_AN"].ToString());
            param.Add("goods_name", Request["goods_name"]);

            int iCheck = DALUtility.Agent.MakeGoods(param);
            if(iCheck == 0)
            {
                return OperationReturn(true, "上架成功");
            }
            return OperationReturn(false, "上架失败");
        }

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:查询所有的代理商商品
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryAgoods()
        {
            string sort = Request["sort"] == null ? "id" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("status_id",1);
            param.Add("agent_AN", Session["Agent_AN"] != null ? Session["Agent_AN"].ToString() : "agent");

            string result = DALUtility.Agent.QryAgoods(param, out int ICount);
            return Content(result);
        }


        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:查询所有的商品
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult GetAllGoodsInfo()
        {
            string sort = Request["sort"] == null ? "GoodsID" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];

            //首先获取前台传递过来的参数
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string goodsName = Request["goods_name"] == null ? "" : Request["goods_name"];
            string goodsIntro = Request["goods_intro"] == null ? "" : Request["goods_intro"];
            decimal goodsPrice = Request["goods_CP"] == null ? 1 : Convert.ToDecimal(Request["goods_CP"]);
            string createBy = Request["goods_BY"] == null ? "" : Request["goods_BY"];
            string createDateTime = Request["goods_CDT"] == null ? "" : Request["goods_CDT"];
            string goodsPic = Request["goods_pic"] == null ? "" : Request["goods_pic"];
            int typeId = Request["type_id"] == null ? 1 : Convert.ToInt32(Request["type_id"]);



            int totalCount;   //输出参数
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["pi"] = pageindex;
            paras["pageSize"] = pagesize;
            paras["goods_name"] = goodsName;
            paras["sort"] = sort;
            paras["order"] = order;
            var goods = DALUtility.Goods.QryGoods<GoodsEntity>(paras, out totalCount);
            return PagerData(totalCount, goods);
        }
        #endregion

        #region _Form
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:查询时段内的报表
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult QryOrder()
        {
            string sort = Request["sort"] == null ? "id" : Request["sort"];
            string order = Request["order"] == null ? "asc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("agent_AN", Session["Agent_AN"].ToString());
            if(Session[" AN"]!= null)
            {
                param.Add("vip_AN", Session[" AN"].ToString());
            }
            return Content(DALUtility.Vip.QryOrder(param, out int iCount));
        }
        #endregion

        #region _自定义
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/25
        /// 功能:添加/修改代理的公用方法
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        //注册或者修改代理信息时,检查邮箱,email,联系方式舒服重复
        public ActionResult save(int ID)
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras["id"] = ID;
            paras["agent_AN"] = Session["Agent_AN"] != null ? Session["Agent_AN"].ToString() : Request["Agent_AN"];
            paras["agent_mp"] = Request["agent_mp"];
            paras["agent_email"] = Request["agent_email"];

            int iCheck = DALUtility.Agent.checkANandMBandEmail(paras);

            if (iCheck > 0)
            {
                return OperationReturn(false, iCheck == 1 ? "所输入的用户名重复,请重新输入!" : (iCheck == 2 ? "所输入的手机号码重复,请重新输入!" : "所输入的邮箱重复,请重新输入!"));
            }
            else
            {
                paras["agent_pwd"] = Request["agent_pwd"];
                paras["agent_CBY"] = Request["agent_CBY"];
                paras["agent_CDT"] = DateTime.Now;
                paras["status_id"] = 1;
                int result = DALUtility.Agent.saveAgent(paras);
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

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/25
        /// 功能:添加/修改vip公用方法
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult saveVIP(int ID)
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
                paras["agent_id"] = Request["agent_id"] == null ? "1" : Request["agent_id"];
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

        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 功能:代理端进行处理充值信息
        /// </summary>
        /// <returns>json值</returns>
        public ActionResult CheckRecharge(int vip_id, decimal recharge_price, DateTime recharge_time)
        {
            return OperationReturn(true, "用户:" + vip_id + "于" + recharge_time + "充值:" + recharge_price + "元!充值成功!!");
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4-28
        /// 修改时间：2019-
        /// 功能：安全退出
        /// </summary>
        public ActionResult RemoveSession()
        {
            Session.RemoveAll();
            return OperationReturn(true,"退出成功!");
        }
        #endregion
    }
}