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
    public class StatusDAL : BaseDal, IStatusDAL
    {
        public int AddStatus(StatusEntity status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbstatus (status_name,status_type)");
            strSql.Append("values");
            strSql.Append("(@StatusName,@StatusType)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@StatusName",status.StatusName),
                new SqlParameter("@StatusType",status.StatusType)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public bool DeleteStatus(string id)
        {
            List<string> list = new List<string>();
            list.Add("delete from tbstatus where id in (" + id + ")");
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

        public bool EditStatus(StatusEntity status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert  tbstatus");
            strSql.Append("status_name=@StatusName,status_type=@StatusType");
            strSql.Append("where id = @StatusID");
            SqlParameter[] paras =
            {
                new SqlParameter("@StatusName",status.StatusName),
                new SqlParameter("@StatusType",status.StatusType),
                new SqlParameter("@StatusID",status.StatusID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<T> QryStatus<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbstatus";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "StatusName", "status_name", "LIKE", "'%'+@StatusName+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbstatus", paras);
        }
    }
}
