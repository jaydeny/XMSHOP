using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    /// <summary>
    ///  字典参数IDAL
    ///  创建人: zxy
    ///  创建时间: 2019年5月23日
    /// </summary>
    public interface IDicDAL
    {
        /// <summary>
        ///  新增与修改
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);

        /// <summary>
        ///  删除/可批量删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        bool DeleteDic(string idList);

        /// <summary>
        ///  根据标识查询
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        IEnumerable<DicEntity> GetDicByTag(int tag);

        /// <summary>
        ///  根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DicEntity GetDicById(int id);
    }
}
