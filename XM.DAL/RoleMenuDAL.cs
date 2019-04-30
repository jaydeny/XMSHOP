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

        public string QryAllRoleMenu<T>(Dictionary<string, object> paras,out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_rolemenu_list";
            GridData grid = new GridData()
            {
                PageIndex = 1,
                PageSize = 100,
                SortField = "Id"
            };
            builder.AddWhereAndParameter(paras, "roleId", "r_id", "=", "@roleId");
            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
    }
}
