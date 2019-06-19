using System.Collections.Generic;
using XM.Model;

namespace XM.IDAL
{
    public interface IBuyDAL
    {
        /// <summary>
        /// 添加购买记录
        /// </summary>
        /// <param name="buy"></param>
        /// <returns></returns>
        int AddBuy(BuyEntity buy);
        /// <summary>
        /// 查询购买记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras">参数</param>
        /// <param name="iCount">输出记录条数</param>
        /// <returns></returns>
        IEnumerable<T> QryBuy<T>(Dictionary<string, object> paras, out int iCount);
        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);
    }
}
