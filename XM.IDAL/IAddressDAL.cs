using System.Collections.Generic;
using XM.Model;

namespace XM.IDAL
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 地址接口
    /// </summary>
    public interface IAddressDAL
    {
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="address">地址类</param>
        /// <returns></returns>
        int AddAddre(AddressEntity address);
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id">地址编号</param>
        /// <returns></returns>
        bool DeleteAddre(string id);
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="address">地址类</param>
        /// <returns></returns>
        bool EditAddre(AddressEntity address);
        /// <summary>
        /// 获取所有地址或者按条件查询
        /// </summary>
        /// <typeparam name="T">地址类</typeparam>
        /// <param name="paras">参数</param>
        /// <param name="iCount">输出记录数量</param>
        /// <returns></returns>
        IEnumerable<T> QryAddre<T>(Dictionary<string, object> paras, out int iCount);
        int Save(Dictionary<string, object> paras);
    }
}
