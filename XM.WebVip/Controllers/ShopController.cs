using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XM.Model;
using XM.WebVIP.Controllers;

namespace XM.WebVip.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Shop
        #region _shopping
        /// <summary>
        /// 作者:曾贤鑫
        /// 日期:2019/4/26
        /// 查看余额
        /// </summary>
        /// <returns></returns>
        public ActionResult Buy()
        {
            if (Session["AN"] == null)
            {
                return OperationReturn(false, "请点击登录页面进行登录");
            }
            else
            {
                //后续需要修改,有关于选中地址的方式
                if (QryAdd() == 0)
                {
                    return OperationReturn(false, "请添加地址后购物");
                }

                ViewData["AcList"] = GetAllAc();

                var vipInfo = QryTOPAdd();
                DateTime date = DateTime.Now;
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("order_date", date);
                param.Add("order_address", vipInfo.AddressID);
                param.Add("order_mp", vipInfo.VipMobliePhone);
                param.Add("vip_AN", Session["AN"].ToString());
                param.Add("agent_AN", Session["Agent_Acc"].ToString());
                param.Add("order_total", decimal.Parse(Request["order_total"]));

                param.Add("buy_time", date);
                param.Add("buy_count", int.Parse(Request["buy_count"]));
                param.Add("buy_AN", Session["AN"].ToString());
                param.Add("agoods_id", int.Parse(Request["agoods_id"]));
                param.Add("buy_total", decimal.Parse(Request["buy_total"]));

                int ChooseAcID = int.Parse(Request["Ac"].ToString());


                List<int> AcResult = Shop(param, ChooseAcID);
                if (AcResult.Contains(1))
                {
                    return OperationReturn(false, AcResult.Contains(1) ? "用户余额不足,请充值后从试!" : "购物出错,请重试!");
                }
                
                return OperationReturn(true, "购物成功");
            }
        }
        
        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-27
        /// 修改时间：2019-
        /// 功能：返回购买是否成功
        /// </summary>
        public List<int> Shop(Dictionary<string, object> OrderAndBuyInfoDic, int ChooseAcID)
        {
            List<int> AcResult = new List<int>();

            ActivityEntity AcEntity = DALUtility.Activity.ActivityEntity<ActivityEntity>(ChooseAcID);

            if (AcEntity != null)
            {
                AcResult.AddRange(ExecuteAcShop(AcEntity, OrderAndBuyInfoDic));
                //AcResult = QryAC(OrderAndBuyInfoDic, AcList);
            }
            else
            {
                int iCheck = DALUtility.Vip.Buy(OrderAndBuyInfoDic);
                AcResult.Add(iCheck);
            }
            return AcResult;
        }

        /// <summary>
        /// 功能:根据活动类型来确定执行的方法
        /// </summary>
        /// <param name="AcList"></param>
        public List<int> ExecuteAcShop(ActivityEntity AcEntity, Dictionary<string, object> OrderAndBuyInfoDic)
        {
            //接收结果的集合
            List<int> intList = new List<int>();

            switch (AcEntity.Ac_type)
            {
                case 1002:
                    //满赠活动的方法
                    intList.AddRange(AcFull(AcEntity, OrderAndBuyInfoDic));
                    break;
                case 1003:
                    //折扣活动的方法
                    intList.AddRange(AcDis(AcEntity, OrderAndBuyInfoDic));
                    break;
            }

            return intList;
        }

        /// <summary>
        /// 功能:满赠活动执行的方法
        /// </summary>
        public List<int> AcFull(ActivityEntity AcItem, Dictionary<string, object> OrderAndBuyInfoDic)
        {
            ParticipationAcEntity PAclist = DALUtility.MDbS.Get<ParticipationAcEntity>("XMShop", "activity", x => x.Vip_AN == AN && x.Ac_id == AcItem.id, null);
            List<int> result = new List<int>();
            switch (PAclist)
            {
                case null:
                    result.Add(AcFullAdd(AcItem, OrderAndBuyInfoDic));
                    break;
                default:
                    result.Add(AcFullUpdate(AcItem, OrderAndBuyInfoDic,PAclist));
                    break;
            }
            return result;
        }

        /// <summary>
        /// 功能:折扣活动的方法
        /// </summary>
        /// <param name="AcItem"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <returns></returns>
        public List<int> AcDis(ActivityEntity AcItem, Dictionary<string, object> OrderAndBuyInfoDic)
        {
            ParticipationAcEntity PAclist = DALUtility.MDbS.Get<ParticipationAcEntity>("XMShop", "activity", x => x.Vip_AN == AN && x.Ac_id == AcItem.id, null);
            List<int> result = new List<int>();

            switch (PAclist)
            {
                case null:
                    result.Add(AcDisAdd(AcItem, OrderAndBuyInfoDic));
                    break;
                default:
                    result.Add(AcDisUpdate(AcItem, OrderAndBuyInfoDic, PAclist));
                    break;
            }
            return result;
        }

        /// <summary>
        /// 功能:满赠活动添加记录到Mongodb的方法
        /// </summary>
        /// <param name="AcItem"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <returns></returns>
        public int AcFullAdd(ActivityEntity AcItem, Dictionary<string, object> OrderAndBuyInfoDic)
        {
            Dictionary<string,object> param = ReturnInfo(AcItem, OrderAndBuyInfoDic);
            CustomFullEntity AcFull = param["AcFull"] as CustomFullEntity;

            ParticipationAcEntity Part = new ParticipationAcEntity()
            {
                Vip_AN = AN,
                Ac_id = AcItem.id,
                Integral_Target = int.Parse(param["AcTarget"].ToString()),
                Integral_now = int.Parse(param["intExpenseIntegral"].ToString()),
                Times = int.Parse(param["TotalTimes"].ToString()),
                Times_now = int.Parse(param["intExpenseTimes"].ToString())
            };

            //将数据添加到mongodb中
            DALUtility.MDbS.Add<ParticipationAcEntity>("XMShop", "activity", Part);
            int iCheck = (int)DALUtility.MDbS.GetCount<NoticEntity>("XMShop", "activity", x => x._id == Part._id);
            if (iCheck == 0)
            {
                return 1;
            }
            else
            {
                RecordAcInfo(OrderAndBuyInfoDic, AcItem.id, AcFull.Minus, AcFull.Ac_type, int.Parse(param["intExpenseTimes"].ToString()));
                return 0;
            }
        }

        /// <summary>
        /// 功能:满赠活动修改记录方法
        /// </summary>
        /// <param name="AcItem"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <param name="PAclist"></param>
        /// <returns></returns>
        public int AcFullUpdate(ActivityEntity AcItem, Dictionary<string, object> OrderAndBuyInfoDic, ParticipationAcEntity PAclist)
        {
            Dictionary<string, object> param = ReturnInfo(AcItem, OrderAndBuyInfoDic);
            CustomFullEntity AcFull = param["AcFull"] as CustomFullEntity;

            //当笔和记录积分相加
            int intUpdateIntegral = int.Parse(param["intExpenseIntegral"].ToString()) + PAclist.Integral_now.Value;

            //当笔和记录次数相加
            int intUpdateTimes = int.Parse(param["intExpenseTimes"].ToString()) + PAclist.Times_now.Value;

            //将要添加进MongoDB的数据
            int NowIntegral;
            if (intUpdateIntegral >= int.Parse(param["AcTarget"].ToString()))
            {
                NowIntegral = intUpdateIntegral % int.Parse(param["AcTarget"].ToString());
                intUpdateTimes += intUpdateIntegral / int.Parse(param["AcTarget"].ToString());
            }
            else
            {
                NowIntegral = intUpdateIntegral;
            }
            //现有的活动次数,加入到mongodb中的数据
            int NowTimes = intUpdateTimes >= int.Parse(param["TotalTimes"].ToString()) ? int.Parse(param["TotalTimes"].ToString()) : intUpdateTimes;
            int ToSqlDB = intUpdateTimes >= int.Parse(param["TotalTimes"].ToString()) ? 
                int.Parse(param["TotalTimes"].ToString()) - PAclist.Times_now.Value :
                intUpdateTimes - PAclist.Times_now.Value;


            PAclist.Integral_now = NowIntegral;
            PAclist.Times_now = NowTimes;
            //将数据添加到mongodb中
            int iCheck = (int)DALUtility.MDbS.Update<ParticipationAcEntity>("XMShop", "activity",
                    x => x._id == PAclist._id, PAclist);
            
            if (iCheck == 0)
            {
                return 1;
            }
            else
            {
                RecordAcInfo(OrderAndBuyInfoDic, AcItem.id, AcFull.Minus, AcFull.Ac_type, ToSqlDB);
                return 0;
            }
        }

        /// <summary>
        /// 用于添加折扣活动记录
        /// </summary>
        /// <param name="AcItem"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <returns></returns>
        public int AcDisAdd(ActivityEntity AcItem, Dictionary<string, object> OrderAndBuyInfoDic)
        {
            CustomDisEntity AcDis = QryACTypeInfoDis<CustomDisEntity>(AcItem.id);
            
            ParticipationAcEntity Part = new ParticipationAcEntity()
            {
                Vip_AN = AN,
                Ac_id = AcItem.id,
                Integral_Target = 0,
                Integral_now = int.Parse(OrderAndBuyInfoDic["order_total"].ToString()),
                Times = int.Parse(AcDis.Times),
                Times_now = 1
            };

            //将数据添加到mongodb中
            DALUtility.MDbS.Add<ParticipationAcEntity>("XMShop", "activity", Part);
            int iCheck = (int)DALUtility.MDbS.GetCount<NoticEntity>("XMShop", "activity", x => x._id == Part._id);
            if (iCheck == 0)
            {
                return 1;
            }
            else
            {
                RecordAcInfo(OrderAndBuyInfoDic, AcItem.id, AcDis.Discount, AcDis.Ac_type, 1);
                return 0;
            }
        }

        /// <summary>
        /// 用于修改折扣活动记录
        /// </summary>
        /// <param name="AcItem"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <param name="PAclist"></param>
        /// <returns></returns>
        public int AcDisUpdate(ActivityEntity AcItem, Dictionary<string, object> OrderAndBuyInfoDic, ParticipationAcEntity PAclist)
        {
            CustomDisEntity AcDis = QryACTypeInfoDis<CustomDisEntity>(AcItem.id);

            //本次消费是否有优惠
            int intExpenseTimes = PAclist.Times_now + 1 <= int.Parse(AcDis.Times) ? 1 : 0;

            PAclist.Times_now += intExpenseTimes;
            PAclist.Integral_now += int.Parse(OrderAndBuyInfoDic["order_total"].ToString());
            //将数据添加到mongodb中
            int iCheck = (int)DALUtility.MDbS.Update<ParticipationAcEntity>("XMShop", "activity",
                    x => x._id == PAclist._id, PAclist);

            if (iCheck == 0)
            {
                return 1;
            }
            else
            {
                RecordAcInfo(OrderAndBuyInfoDic, AcItem.id, AcDis.Discount, AcDis.Ac_type, intExpenseTimes);
                return 0;
            }
        }

        /// <summary>
        /// 功能:向tbAc_order和tbReceiveAward添加记录
        /// </summary>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <param name="Ac_id"></param>
        /// <param name="Award">奖励</param>
        /// <param name="Code">标识</param>
        /// <param name="Times">次数</param>
        /// <returns></returns>
        public int RecordAcInfo(Dictionary<string, object> OrderAndBuyInfoDic, int Ac_id, object Award, int Code, int Times)
        {
            DateTime date = DateTime.Now;
            Dictionary<string, object> FullAc = new Dictionary<string, object>();
            FullAc.Add("Code", Code);
            FullAc.Add("Ac_id", Ac_id);
            FullAc.Add("Agoods_id", OrderAndBuyInfoDic["agoods_id"]);
            FullAc.Add("Vip_AN", AN);
            FullAc.Add("Agent_AN", Agent_Acc);
            FullAc.Add("Integral", OrderAndBuyInfoDic["order_total"]);
            FullAc.Add("Date", date);
            FullAc.Add("Award", Award);
            FullAc.Add("Status_id", 1004);
            FullAc.Add("Times", Times);

            FullAc.Add("order_date", date);
            FullAc.Add("order_address", OrderAndBuyInfoDic["order_address"]);
            FullAc.Add("order_mp", OrderAndBuyInfoDic["order_mp"]);
            FullAc.Add("order_total", decimal.Parse(Request["order_total"]));

            FullAc.Add("buy_time", date);
            FullAc.Add("buy_count", int.Parse(Request["buy_count"]));
            FullAc.Add("buy_AN", Session["AN"].ToString());
            FullAc.Add("buy_total", decimal.Parse(Request["buy_total"]));

            return DALUtility.Vip.BuyWithAc(FullAc);
        }

        /// <summary>
        /// 返回公用的参数
        /// </summary>
        /// <param name="AcItem"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <returns></returns>
        public Dictionary<string,object> ReturnInfo(ActivityEntity AcItem, Dictionary<string, object> OrderAndBuyInfoDic)
        {
            CustomFullEntity AcFull = QryACTypeInfoFull<CustomFullEntity>(AcItem.id);
            //目标积分
            int AcTarget = int.Parse(AcFull.Ac_full);
            //总次数
            int TotalTimes = int.Parse(AcFull.Times);
            //当笔消费总金额
            int intExpense = int.Parse(OrderAndBuyInfoDic["order_total"].ToString());
            //当笔消费优惠可用次数
            int intExpenseTimes = intExpense / AcTarget < TotalTimes ? intExpense / AcTarget : TotalTimes;
            //当笔消费优惠后余下积分
            int intExpenseIntegral = intExpense % AcTarget;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AcFull", AcFull);
            param.Add("AcTarget", AcTarget);
            param.Add("TotalTimes", TotalTimes);
            param.Add("intExpense", intExpense);
            param.Add("intExpenseTimes", intExpenseTimes);
            param.Add("intExpenseIntegral", intExpenseIntegral);
            return param;
        }
        #endregion

        #region _order
        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-
        /// 修改时间：2019-
        /// 功能：查询订单
        /// </summary>
        public ActionResult QryOrder()
        {
            string sort = Request["sort"] == null ? "OrderID" : Request["sort"];
            string order = Request["order"] == null ? "desc" : Request["order"];
            int pageindex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pagesize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            int iCount;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pi", pageindex);
            param.Add("pageSize", pagesize);
            param.Add("sort", sort);
            param.Add("order", order);
            param.Add("agent_AN", Session["Agent_Acc"].ToString());
            param.Add("vip_AN", Session["AN"].ToString());
            var objOrder = DALUtility.Order.QryOrder<OrderEntity>(param, out iCount);
            return PagerData(iCount,objOrder);
        }
        #endregion

        #region _自定义
        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4/30
        /// 修改时间：2019-
        /// 功能：查询地址
        /// </summary>
        public int QryAdd()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_AN", Session["AN"].ToString());
            var vipInfo = DALUtility.Vip.QryAdd<int>(param);
            return vipInfo;
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4/30
        /// 修改时间：2019-
        /// 功能：查询地址
        /// </summary>
        public VipInfoDTO QryTOPAdd()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("vip_AN", Session["AN"].ToString());
            var vipInfo = DALUtility.Vip.QryTOPAdd<VipInfoDTO>(param);
            return vipInfo;
        }

        /// <summary>
        /// 获取活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public T QryACTypeInfoFull<T>(int Ac_id)
        {
            T result = DALUtility.Activity.QryACTypeInfoFull<T>(Ac_id);
            return result;
        }

        /// <summary>
        /// 获取活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public T QryACTypeInfoDis<T>(int Ac_id)
        {
            T result = DALUtility.Activity.QryACTypeInfoDis<T>(Ac_id);
            return result;
        }

        public List<ActivityEntity> GetAllAc()
        {
            Dictionary<string, object> AcDic = new Dictionary<string, object>();
            AcDic.Add("agent_AN", Agent_Acc);
            AcDic.Add("Date", DateTime.Now);

            return DALUtility.Activity.QryAC<ActivityEntity>(AcDic);
        }
        #endregion
    }
}