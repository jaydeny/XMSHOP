using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.IDAL
{
    public interface IActivityDAL
    {
        /// <summary>
        /// 功能:获取当前登录会员可以参与的活动
        /// </summary>
        /// <typeparam name="ActivityEntity"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        List<ActivityEntity> QryAC<ActivityEntity>(Dictionary<string, object> param);

        /// <summary>
        /// 功能:查询活动的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        ActivityEntity ActivityEntity<ActivityEntity>(int Ac_id);

        /// <summary>
        /// 功能:查询单一活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        CustomFullEntity QryACTypeInfoFull<CustomFullEntity>(int Ac_id);

        /// <summary>
        /// 功能:查询单一活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        CustomDisEntity QryACTypeInfoDis<CustomDisEntity>(int Ac_id);

        /// <summary>
        /// 功能:满赠类型活动的订单记录和奖励发放
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int RecordAcInfo(Dictionary<string, object> param);
    }
}
