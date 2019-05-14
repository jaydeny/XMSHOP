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
    public class RechargeDAL : BaseDal, IRechargeDAL
    {
        public int AddRecharge(RechargeEntity recharge)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbrecharge (recharge_name,recharge_price,recharge_time,agent_id,vip_id)");
            strSql.Append("values");
            strSql.Append("(@RechargeName,@RechargePrice,@RechargeTime,@AgentID,@VipID)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@RechargeName",recharge.RechargeName),
                new SqlParameter("@RechargePrice",recharge.RechargePrice),
                new SqlParameter("@RechargeTime",recharge.RechargeTime),
                new SqlParameter("@AgentID",recharge.AgentID),
                new SqlParameter("@VipID",recharge.VipID)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }


        public IEnumerable<T> QryRecharge<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_recharge_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "AgentID", "AgentID", "=", "@AgentID");
            builder.AddWhereAndParameter(paras, "VipID", "VipID", "=", "@VipID");
            builder.AddWhereAndParameter(paras, "startTime", "REchargeTime", ">");
            builder.AddWhereAndParameter(paras, "endTime", "REchargeTime", "<");
            return SortAndPage<T>(builder, grid, out iCount);
        }
        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbrecharge", paras);
        }
    }
}
