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
                
                List<int> AcResult = Shop(param);
                if (!AcResult.Contains(0))
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
        public List<int> Shop(Dictionary<string, object> param)
        {
            List<int> AcResult = new List<int>();

            Dictionary<string, object> AcDic = new Dictionary<string, object>();
            AcDic.Add("agent_AN", Agent_Acc);
            AcDic.Add("Date", DateTime.Now);

            List<ActivityEntity> AcList = DALUtility.Activity.QryAC<ActivityEntity>(AcDic);
            if (AcList.Count != 0)
            {
                AcResult = QryAC(param, AcList);
            }
            else
            {
                int iCheck = DALUtility.Vip.Buy(param);
                AcResult.Add(iCheck);
            }
            return AcResult;
        }

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
        #endregion

        #region _活动相关
        /// <summary>
        /// 功能:返回符合要求的活动
        /// </summary>
        public List<int> QryAC(Dictionary<string, object> OrderAndBuyInfoDic, List<ActivityEntity> AcList)
        {
            List<ParticipationAcEntity> PAclist = DALUtility.MDbS.List<ParticipationAcEntity>("XMShop", "activity", x => x.Vip_AN == AN, null, null);

            return FullAction(AcList, PAclist, OrderAndBuyInfoDic);
            
        }

        /// <summary>
        /// 活动的具体方法
        /// </summary>
        /// <param name="AcList"></param>
        /// <param name="PAclist"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <returns></returns>
        public List<int> FullAction(List<ActivityEntity> AcList, List<ParticipationAcEntity> PAclist, Dictionary<string, object> OrderAndBuyInfoDic)
        {
            int nowIntegral = int.Parse(OrderAndBuyInfoDic["order_total"].ToString());
            List<int> intResult = new List<int>();
            if (PAclist.Count != 0)
            {
                foreach (var item in PAclist)
                {
                    intResult = getRecordAcInfoResult(nowIntegral, AcList, OrderAndBuyInfoDic, item);
                }
            }
            else
            {
                intResult = getRecordAcInfoResult(nowIntegral, AcList, OrderAndBuyInfoDic);
            }
            return intResult;
        }

        /// <summary>
        /// 因为活动可能有多个,所以有一个区分的方法
        /// </summary>
        /// <param name="nowIntegral"></param>
        /// <param name="AcList"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<int> getRecordAcInfoResult(int nowIntegral, List<ActivityEntity> AcList, Dictionary<string, object> OrderAndBuyInfoDic, ParticipationAcEntity item = null)
        {
            //结果的集合
            List<int> intResult = new List<int>();

            for (int i = 0; i < AcList.Count; i++)
            {
                if (AcList[i].Ac_type == 1002)
                {
                    intResult = FullResult(nowIntegral,AcList[i], OrderAndBuyInfoDic,item );
                }
                else if (AcList[i].Ac_type == 1003)
                {

                }
            }
            return intResult;
        }

        /// <summary>
        /// 获取插入数据库的结束,主要是满赠活动使用这个方法
        /// </summary>
        /// <param name="nowIntegral"></param>
        /// <param name="AcItem"></param>
        /// <param name="OrderAndBuyInfoDic"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<int> FullResult(int nowIntegral, ActivityEntity AcItem, Dictionary<string, object> OrderAndBuyInfoDic, ParticipationAcEntity item = null)
        {
            //结果的集合
            List<int> intResult = new List<int>();
            //结果
            long iCheck = 0;

            //获取活动的信息,type,溢满值,增送值
            CustomFullEntity Full = QryACTypeInfo<CustomFullEntity>(AcItem.id);

            //余下积分 4100%1000=100
            int remainingPoints = nowIntegral % int.Parse(Full.Ac_full);
            //如果item为null,则返回0,是添加的情况
            int nowCount = item != null ? item.PresentCount.Value : 0;
            //有效次数
            int EffectiveNumber = (nowIntegral / int.Parse(Full.Ac_full))+ nowCount > int.Parse(Full.Times)  ? int.Parse(Full.Times)-item.PresentCount.Value : nowIntegral / int.Parse(Full.Ac_full);
            //返回次数,用min函数对比,返回最小值
            int intTimes = Math.Min(int.Parse(Full.Times), item != null ? item.PresentCount.Value + EffectiveNumber : EffectiveNumber);
            if ( item != null)
            {
                if (item.PresentNow+ remainingPoints >= item.ActTarget)
                {
                    Dictionary<string, object> TimesAndPresentNow = CheckTimesAndPresentNow(intTimes, item.PresentNow.Value+ remainingPoints, item.ActTarget.Value);
                    //现有积分
                    item.PresentNow = int.Parse(TimesAndPresentNow["PresentNow"].ToString());
                    //有效次数
                    item.PresentCount = int.Parse(TimesAndPresentNow["intTimes"].ToString());
                    EffectiveNumber++;
                }
                else
                {
                    //现有积分
                    item.PresentNow = item.PresentNow + remainingPoints;
                    //有效次数
                    item.PresentCount = intTimes;
                }
                //将数据添加到mongodb中
                iCheck = DALUtility.MDbS.Update<ParticipationAcEntity>("XMShop", "activity",
                    x => x._id == item._id,
                    item);
                //将数据添加到sqlserver中
                intResult.Add(RecordAcInfo(OrderAndBuyInfoDic, AcItem.id, int.Parse(Full.Minus), Full.Ac_type, EffectiveNumber));
            }
            else
            {
                //将数据添加到mongodb中
                DALUtility.MDbS.Add<ParticipationAcEntity>("XMShop", "activity",
                        new ParticipationAcEntity()
                        {
                            Vip_AN = AN,
                            ActID = AcItem.id,
                            ActTarget = int.Parse(Full.Ac_full),
                            PresentNow = remainingPoints,
                            Total = int.Parse(Full.Times),
                            PresentCount = intTimes
                        });
                //将数据添加到sqlserver中
                intResult.Add(RecordAcInfo(OrderAndBuyInfoDic, AcItem.id, int.Parse(Full.Minus), Full.Ac_type, EffectiveNumber));
            }
            return intResult;
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
            FullAc.Add("Vip_AN",AN);
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
        /// 获取活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public T QryACTypeInfo<T>(int Ac_id)
        {
            T result = DALUtility.Activity.QryACTypeInfo<T>(Ac_id);
            return result;
        }

        /// <summary>
        /// 如果mongodb中的当前积分+剩余积分大于目标积分的处理
        /// </summary>
        /// <param name="intTimes"></param>
        /// <param name="PresentNow"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Dictionary<string,object> CheckTimesAndPresentNow(int intTimes, int PresentNow,  int target)
        {
            intTimes += PresentNow / target;
            PresentNow = PresentNow % target;
            Dictionary<string, object> TimesAndPresentNow = new Dictionary<string, object>();
            TimesAndPresentNow.Add("intTimes", intTimes);
            TimesAndPresentNow.Add("PresentNow", PresentNow);
            return TimesAndPresentNow;

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
    }
}