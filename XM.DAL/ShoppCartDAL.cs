using System.Collections;
using System.Collections.Generic;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class ShoppCartDAL : BaseDal , IShoppCartDAL
    {
       
        public IEnumerable<ShoppCartEntity> QryDataByVIPID(int vipID)
        {
            string sql = "select * from tbShoppCart sc inner join tbAgoods agoods on sc.Agoods_ID = agoods.id where vip_ID = @id";
            return QueryList<ShoppCartEntity>(sql, new { id = vipID });
        }
    }
}
