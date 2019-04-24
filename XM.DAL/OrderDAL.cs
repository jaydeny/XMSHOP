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
    public class OrderDAL : BaseDal, IOrderDAL
    {
        public int AddOrder(OrderEntity order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tborder (order_date,order_address,order_mp,vip_AN,agent_AN,order_total)");
            strSql.Append("values");
            strSql.Append("(@OrderDate,@AddressID,@OrderMP,@VipAccountName,@AgentAccountName,@OrderPrice)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@OrderDate",order.OrderDate),
                new SqlParameter("@AddressID",order.AddressID),
                new SqlParameter("@OrderMP",order.OrderMP),
                new SqlParameter("@VipAccountName",order.VipAccountName),
                new SqlParameter("@AgentAccountName",order.AgentAccountName),
                new SqlParameter("@OrderPrice",order.OrderPrice)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public bool DeleteOrder(string id)
        {
            List<string> list = new List<string>();
            list.Add("delete from tborder where id in (" + id + ")");
            try
            {
                int count = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, list);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EditOrder(OrderEntity order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  tborder set");
            strSql.Append("order_date=@OrderDate,order_address=@AddressID,order_mp=@OrderMP,vip_AN=@VipAccountName,agent_AN=@AgentAccountName,order_total=@OrderPrice");
            strSql.Append("where id=@OrderID");
            SqlParameter[] paras =
            {
                new SqlParameter("@OrderDate",order.OrderDate),
                new SqlParameter("@AddressID",order.AddressID),
                new SqlParameter("@OrderMP",order.OrderMP),
                new SqlParameter("@VipAccountName",order.VipAccountName),
                new SqlParameter("@AgentAccountName",order.AgentAccountName),
                new SqlParameter("@OrderPrice",order.OrderPrice),
                new SqlParameter("@OrderID",order.OrderID)
            };
            throw new NotImplementedException();
        }

        public IEnumerable<T> QryOrder<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_order_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "vip_AN", "VipAccountName", "LIKE", "'%'+@vip_AN+'%'");
            builder.AddWhereAndParameter(paras, "agent_AN", "AgentAccountName", "LIKE", "'%'+@agent_AN+'%'");
            builder.AddWhereAndParameter(paras, "startTime", "OrderDate", ">");
            builder.AddWhereAndParameter(paras, "endTime", "OrderDate", "<");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tborder", paras);
        }
    }
}
