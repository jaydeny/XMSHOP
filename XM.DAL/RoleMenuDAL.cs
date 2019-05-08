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

        public IEnumerable<T> QryAllRoleMenu<T>(Dictionary<string, object> paras)
        {
            string strSql = "SELECT r.*,m.Controller AS Controller,m.Action AS Action " +
                "FROM v_rolemenu_list r,v_menu_list m " +
                "where r.MenuId = m.Id and r.Id=@r_id";
            return QueryList<T>(strSql, paras);
        }
    }
}
