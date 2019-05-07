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
    public class GoodsTypeDAL : BaseDal, IGoodsTypeDAL
    {
        public int AddType(GoodsTypeEntity goodsType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbtype (type_name)");
            strSql.Append("values");
            strSql.Append("(@TypeName)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@TypeName",goodsType.TypeName)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public bool DeleteType(string id)
        {
            List<string> list = new List<string>();
            list.Add("delete from tbtype where id in (" + id + ")");
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

        public bool EditType(GoodsTypeEntity goodsType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  tbtype set");
            strSql.Append("type_name=@TypeName");
            strSql.Append("where id=@TypeID");
            SqlParameter[] paras =
            {
                new SqlParameter("@TypeName",goodsType.TypeName),
                new SqlParameter("@TypeID",goodsType.TypeID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public GoodsTypeEntity GetTypeById(string id)
        {
            const string strSql = "select * from v_type_list where typeID = @ID";
            return QuerySingle<GoodsTypeEntity>(strSql, new { ID = id });
        }

        public IEnumerable<T> QryType<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_type_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "TypeName", "type_name", "LIKE", "'%'+@TypeName+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbtype", paras);
        }
    }
}
