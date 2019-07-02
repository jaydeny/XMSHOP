using System.Collections.Generic;
using XM.DAL.comm;
using XM.IDAL;

namespace XM.DAL
{
    public class RoleMenuDAL : BaseDal, IRoleMenuDAL
    {

        /// <summary>
        /// 查询角色列表并分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <param name="iCount">总条数</param>
        /// <returns></returns>
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
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public IEnumerable<T> QryRoleMenu<T>(Dictionary<string, object> paras)
        {
            string strSql = "SELECT r.*,m.Controller AS Controller,m.Action AS Action,m.Name,m.ParentId " +
                "FROM v_rolemenu_list r,v_menu_list m " +
                "where r.MenuId = m.Id and r.Id=@r_id";
            return QueryList<T>(strSql, paras);
        }
    }
}
