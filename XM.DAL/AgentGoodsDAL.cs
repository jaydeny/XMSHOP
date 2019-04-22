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
    public class AgentGoodsDAL : BaseDal, IAgentGoodsDAL
    {
        public int AddGoods(AgentGoodsEntity goods)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbAGoods (goods_id,status_id,price,up_time,agent_AN)");
            strSql.Append("values");
            strSql.Append("(@GoodsID,@StatusID,@Price,@UPTime,@AgentAccountName)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@GoodsID",goods.GoodsID),
                new SqlParameter("@StatusID",goods.StatusID),
                new SqlParameter("@Price",goods.Price),
                new SqlParameter("@UPTime",goods.UPTime),
                new SqlParameter("@AgentAccountName",goods.AgentAccountName)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public bool DeleteGoods(AgentGoodsEntity goods)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbAGoods set");
            strSql.Append("status_id=@StatusID");
            strSql.Append("where Agoods_id = @AgentGoodsId");
            SqlParameter[] paras =
            {
                new SqlParameter("@StatusID",goods.StatusID),
                new SqlParameter("@AgentGoodsId",goods.AgentGoodsID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public bool EditGoods(AgentGoodsEntity goods)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbAGoods set");
            strSql.Append("goods_id=@GoodsId,status_id=@StatusID,price=@Price,up_time=@UPTime,agent_AN=@AgentAccountName");
            strSql.Append("where Agoods_id = @AgentGoodsID");
            SqlParameter[] paras =
            {
                new SqlParameter("@GoodsId",goods.GoodsID),
                new SqlParameter("@StatusID",goods.StatusID),
                new SqlParameter("@Price",goods.Price),
                new SqlParameter("@UPTime",goods.UPTime),
                new SqlParameter("@AgentAccountName",goods.AgentAccountName),
                new SqlParameter("@AgentGoodsID",goods.AgentGoodsID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<T> QryGoods<T>(Dictionary<string, object> paras, out int iCount)
        {
            throw new NotImplementedException();
        }

        public T QryGoodsInfo<T>(Dictionary<string, object> paras)
        {
            throw new NotImplementedException();
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbAGoods", paras);
        }
    }
}
