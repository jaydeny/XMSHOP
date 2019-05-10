﻿using System;
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
            return StandarInsertOrUpdate("tbtype", paras);
        }
    }
}
