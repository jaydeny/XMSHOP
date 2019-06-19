using System.Collections.Generic;
using XM.Model;

namespace XM.IDAL
{
    public interface IRechargeDAL
    {
        /// <summary>
        /// 添加充值记录
        /// </summary>
        /// <param name="recharge"></param>
        /// <returns></returns>
        int AddRecharge(RechargeEntity recharge);
        /// <summary>
        /// 查询充值记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        IEnumerable<T> QryRecharge<T>(Dictionary<string, object> paras, out int iCount);
        /// <summary>
        /// 添加和修改充值记录
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);

    }
}
