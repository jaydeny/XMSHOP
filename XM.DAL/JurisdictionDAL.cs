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
    public class JurisdictionDAL : BaseDal, IJurisdictionDAL
    {
        public int AddJurisdiction(JurisdictionEntity jurisdiction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbtype (jurisdition_name)");
            strSql.Append("values");
            strSql.Append("(@JurisdictionName)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@JurisdictionName",jurisdiction.JurisdictionName)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public bool DeleteJurisdiction(string id)
        {
            List<string> list = new List<string>();
            list.Add("delete from tbjurisdiction where id in (" + id + ")");
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

        public bool EditJurisdiction(JurisdictionEntity jurisdiction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbjurisdiction set");
            strSql.Append("(jurisdition_name=@JurisdictionName)");
            strSql.Append("where id=@JurisdictionID");
            SqlParameter[] paras =
            {
                new SqlParameter("@JurisdictionName",jurisdiction.JurisdictionName),
                new SqlParameter("@JurisdictionID",jurisdiction.JurisdictionID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<T> QryJurisdiction<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbjurisdiction";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "Jurisdiction", "jurisdition_name", "LIKE", "'%'+@Jurisdiction+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbjurisdiction", paras);
        }
    }
}
