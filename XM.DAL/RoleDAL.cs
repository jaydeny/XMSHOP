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
    public class RoleDAL : BaseDal, IRoleDAL
    {
        public int AddRole(RoleEntity role)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbrole (role_name,jurisdiction_id)");
            strSql.Append("values");
            strSql.Append("(@RoleNamem,@Jurisdiction)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@RoleName",role.RoleName),
                new SqlParameter("@Jurisdiction",role.Jurisdiction)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public bool DeleteRole(string id)
        {
            List<string> list = new List<string>();
            list.Add("delete from tbrole where id in (" + id + ")");
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

        public bool EditRole(RoleEntity role)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert  tbrole set");
            strSql.Append("role_name=@RoleNamem,jurisdiction_id=@Jurisdiction");
            strSql.Append("where id = @RoleID");
            SqlParameter[] paras =
            {
                new SqlParameter("@RoleName",role.RoleName),
                new SqlParameter("@Jurisdiction",role.Jurisdiction),
                new SqlParameter("@RoleID",role.RoleID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<T> QryRole<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_role_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "RoleName", "role_name", "LIKE", "'%'+@RoleName+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbrole", paras);
        }
    }
}
