using Newtonsoft.Json;
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
    public class MenuDAL : BaseDal, IMenuDAL
    {

        public int AddMenu(MenuEntity menu)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbrole (name,code,state,controller,action,parentid,sortvalue)");
            strSql.Append("values");
            strSql.Append("(@RoleNamem,@Code,@State,@Controller,@Action,@ParentId,@SortValue)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@RoleName",menu.Name),
                new SqlParameter("@Code",menu.Code),
                new SqlParameter("@State",menu.State),
                new SqlParameter("@Controller",menu.Controller),
                new SqlParameter("@Action",menu.Action),
                new SqlParameter("@ParentId",menu.ParentId),
                new SqlParameter("@SortValue",menu.SortValue)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public bool DeleteMenu(string id)
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

        public bool EditMenu(MenuEntity menu)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  tbrole set");
            strSql.Append("name=@RoleName,code=@Code,state=@State,controller=@Controller,action=@Action,parentid=@ParentId,sortvalue=@SortValue");
            strSql.Append("where id = @RoleID");
            SqlParameter[] paras =
            {
                new SqlParameter("@RoleName",menu.Name),
                new SqlParameter("@Code",menu.Code),
                new SqlParameter("@State",menu.State),
                new SqlParameter("@Controller",menu.Controller),
                new SqlParameter("@Action",menu.Action),
                new SqlParameter("@ParentId",menu.ParentId),
                new SqlParameter("@SortValue",menu.SortValue),
                new SqlParameter("@RoleID",menu.Id)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<T> GetAllMenu<T>(Dictionary<string, object> paras,out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_roleMenu_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "Rid", "r_id", "=", "@Rid");
            return SortAndPage<T>(builder, grid, out iCount);

        }
    }
}
