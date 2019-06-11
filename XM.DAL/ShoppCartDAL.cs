using System.Collections;
using System.Collections.Generic;
using System.Data;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class ShoppCartDAL : BaseDal , IShoppCartDAL
    {
        /// <summary>
        /// 编辑购物车项
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int EditCart(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbShoppCart_editCart", paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 根据用户ID获取购物车
        /// </summary>
        /// <param name="vipID"></param>
        /// <returns></returns>
        public IEnumerable<ShoppCartEntity> QryDataByVIPID(int vipID)
        {
            string sql = "select * from tbShoppCart sc inner join tbAgoods agoods on sc.Agoods_ID = agoods.id where vip_ID = @id";
            return QueryList<ShoppCartEntity>(sql, new { id = vipID });
        }
    }
}
