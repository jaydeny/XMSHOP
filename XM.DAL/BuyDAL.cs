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
    public class BuyDAL : BaseDal, IBuyDAL
    {
        public int AddBuy(BuyEntity buy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbbuy (buy_time,buy_count,buy_AN,goods_id,buy_total,order_id)");
            strSql.Append("values");
            strSql.Append("(@BuyTime,@BuyCount,@BuyAccountName,@GoodsID,@BuyPrice,@OrderID,@TypeId)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@BuyTime",buy.BuyTime),
                new SqlParameter("@BuyCount",buy.BuyCount),
                new SqlParameter("@BuyAccountName",buy.BuyAccountName),
                new SqlParameter("@GoodsID",buy.GoodsID),
                new SqlParameter("@BuyPrice",buy.BuyPrice),
                new SqlParameter("@OrderID",buy.OrderID)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public IEnumerable<T> QryBuy<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_Buy_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "BuyAccoutName", "buy_AN", "LIKE", "'%'+@BuyAccoutName+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbBuy", paras);
        }
    }
}
