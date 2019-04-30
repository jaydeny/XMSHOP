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
            builder.AddWhereAndParameter(paras, "name", "Name", "Like", "%@id%");
            return SortAndPage<T>(builder, grid, out iCount);
        }


        public List<RoleMenuEntity> GetAllMenuById(List<int> MenuIds)
        {
            string strSql = "select * from v_menu_list where id = @ID";
            List<RoleMenuEntity> menus = new List<RoleMenuEntity>();
            foreach(int id in Ids)
            {
                menus.Add(QuerySingle<RoleMenuEntity>(strSql, new { ID = id }));
            }
            return menus;
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandarInsertOrUpdate("tbmenu", paras);
        }
    }
}
