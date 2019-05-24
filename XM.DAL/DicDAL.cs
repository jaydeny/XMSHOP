using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    /// <summary>
    ///  字典参数DAL
    ///  创建人: zxy
    ///  创建时间: 2019年5月23日
    /// </summary>
    public class DicDAL : BaseDal, IDicDAL
    {
        /// <summary>
        ///  新增与修改
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int Save(Dictionary<string, object> paras)
        {
            return StandarInsertOrUpdate("tbdic", paras);
        }
        
        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteDic(string idList)
        {
            return Execute("DELETE FROM tbdic WHERE id IN (" + idList + ")", null, CommandType.Text) > 0;
        }

        /// <summary>
        ///  根据标识查询
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public IEnumerable<DicEntity> GetDicByTag(int tag)
        {
            string sql = "SELECT * FROM tbdic WHERE tag = @tag";
            return QueryList<DicEntity>(sql, new { tag });
        }

        /// <summary>
        ///  根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DicEntity GetDicById(int id)
        {
            string sql = "SELECT * FROM tbdic WHERE id = @id";
            return QuerySingle<DicEntity>(sql, new { id });
        }

    }
}
