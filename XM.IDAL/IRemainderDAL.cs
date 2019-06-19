using System.Collections.Generic;
using XM.Model;

namespace XM.IDAL
{
    public interface IRemainderDAL
    {
        /// <summary>
        /// 修改余额
        /// </summary>
        /// <param name="remainder"></param>
        /// <returns></returns>
        bool EditRemainder(RemainderEntity remainder);
        /// <summary>
        /// 查询当前余额
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        IEnumerable<T> QryRemainder<T>(Dictionary<string, object> paras, out int iCount);
        /// <summary>
        /// 添加和修改余额
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);
    }
}
