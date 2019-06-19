using System;
using System.Collections.Generic;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class MenuDAL : BaseDal, IMenuDAL
    {


        public bool DeleteMenu(string id)
        {
            List<string> list = new List<string>();
            list.Add("delete from tbmenu where id in (" + id + ")");
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


        public IEnumerable<T> GetAllMenu<T>(Dictionary<string, object> paras,out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_menu_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "id", "Id", "=", "@id");
            builder.AddWhereAndParameter(paras, "name", "Name", "Like", "'%'+@name+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }


        public List<MenuEntity> GetAllMenuByIds(List<int> Ids)
        {
            string strSql = "select * from v_menu_list where Id = @ID";
            List<MenuEntity> menus = new List<MenuEntity>();
            foreach(int id in Ids)
            {
                menus.Add(QuerySingle<MenuEntity>(strSql, new { ID = id }));
            }
            return menus;
        }
        public MenuEntity GetMenuById(string Id)
        {
            string strSql = "select * from v_menu_list where Id = @ID";
            return QuerySingle<MenuEntity>(strSql, new { ID = Id });
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandarInsertOrUpdate("tbMenu", paras);
        }
        public IEnumerable<T> QryAllMenu<T>()
        {
            string strSql = "select * from v_menu_list";
            return QueryList<T>(strSql);
        }
    }
}
