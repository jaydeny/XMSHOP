using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class RoleMenuDAL : BaseDal, IRoleMenuDAL
    {

        public IEnumerable<T> QryAllRoleMenu<T>(Dictionary<string, object> paras,out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_rolemenu_list";
            GridData grid = new GridData()
            {
                PageIndex = 1,
                PageSize = 100,
                SortField = "Id"
            };
            builder.AddWhereAndParameter(paras, "roleId", "Id", "=", "@roleId");
            //var s = 
            //string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return SortAndPage<T>(builder, grid, out iCount);
        }
        public IEnumerable<T> QryRoleMenu<T>(Dictionary<string, object> paras)
        {
            string strSql = "SELECT r.*,m.Controller AS Controller,m.Action AS Action " +
                "FROM v_rolemenu_list r,v_menu_list m " +
                "where r.MenuId = m.Id and r.Id=@r_id";
            return QueryList<T>(strSql, paras);
        }
    }
}
