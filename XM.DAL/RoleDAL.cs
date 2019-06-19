using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class RoleDAL : BaseDal, IRoleDAL
    {


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

        public RoleEntity GetRoleById(string id)
        {
            string strSql = "select * from v_role_list where Id = @ID";
            return QuerySingle<RoleEntity>(strSql, new { ID = id });
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
            builder.AddWhereAndParameter(paras, "Name", "name", "LIKE", "'%'+@Name+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }
        
        public int Save(Dictionary<string, object> paras)
        {
            DataTable dtRolememu = paras["rolemenu"] as DataTable;
            paras["rolemenu"] = dtRolememu.AsTableValuedParameter();
            return QuerySingle<int>("P_Role_Save", paras, CommandType.StoredProcedure); 
                //StandarInsertOrUpdate("tbrole", paras);
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> QryRole<T>()
        {
            string strSql = "select * from v_role_list";
            return QueryList<T>(strSql);
        }


    }
}
