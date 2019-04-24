using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class RemainderDAL : BaseDal, IRemainderDAL
    {
        public bool EditRemainder(RemainderEntity remainder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  tbremainder set");
            strSql.Append("remainder=@Remainder");
            strSql.Append("where vip_id=@VipID");
            SqlParameter[] paras =
            {
                new SqlParameter("@Remainder",remainder.Remainder),
                new SqlParameter("@VipID",remainder.VipID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<T> QryRemainder<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_remainder_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "VipID", "vip_id");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbremainder", paras);
        }
    }
}
