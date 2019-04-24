﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace XM.DAL.comm
{
    public class BaseDal
    {
        private static int CommandTimeout
        {
            get
            {
                int timeoutSecond = 0;

                if (int.TryParse(ConfigurationManager.AppSettings["CommandTimeout"], out timeoutSecond))
                {
                    return timeoutSecond;
                }

                return 30;
            }
        }

        protected string ConnString { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected IDbConnection GetConnection()
        {
            return new SqlConnection(ConnString);
        }

        protected T QuerySingle<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = GetConnection())
            {
                return conn.QuerySingle<T>(sql, param, null, CommandTimeout, commandType);
            }
        }

        protected int Execute(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = GetConnection())
            {
                return conn.Execute(sql, param, null, CommandTimeout, commandType);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="builder"></param>
        /// <param name="grid"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        protected IEnumerable<object> SortAndPage(WhereBuilder builder, GridData grid, out int iCount)
        {
            iCount = 0;
            var sql = "";
            var countSql = "";
            FormartSqlToSortAndPage(grid, ref sql, ref countSql, ref builder);
            using (IDbConnection dbConnection = GetConnection())
            {
                var retObj = dbConnection.Query(sql, builder.Parameters);
                iCount = dbConnection.QuerySingleOrDefault<int>(countSql, builder.Parameters);
                return retObj;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="builder"></param>
        /// <param name="grid"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        protected IEnumerable<T> SortAndPage<T>(WhereBuilder builder, GridData grid, out int iCount)
        {
            iCount = 0;
            var sql = "";
            var countSql = "";
            FormartSqlToSortAndPage(grid, ref sql, ref countSql, ref builder);
            using (IDbConnection dbConnection = GetConnection())
            {
                var retObj = dbConnection.Query<T>(sql, builder.Parameters);
                iCount = dbConnection.QuerySingleOrDefault<int>(countSql, builder.Parameters);
                return retObj;
            }
        }

        /// <summary>
        /// 执行标准单表Insert&Update操作
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tabName">表名</param>
        /// <param name="paras">参数</param>
        /// <param name="keyFild">主键字段</param>
        /// <returns></returns>
        protected int StandardInsertOrUpdate(string tabName, Dictionary<string, object> paras, string keyFild = "id")
        {
            var fields = GetFieldsFromDictionary(paras, keyFild);
            var sql = "";
            if (paras[keyFild].ToString().Equals("0"))
            {
                var fieldsSql1 = String.Join(",", fields);
                var fieldsSql2 = String.Join(",", fields.Select(field => "@" + field));
                sql = String.Format("INSERT {0} ({1}) VALUES ({2});", tabName, fieldsSql1, fieldsSql2);
            }
            else
            {
                var fieldsSql = String.Join(",", fields.Select(field => field + " = @" + field));
                sql = String.Format("UPDATE {0} SET {1} WHERE {2} = @{2}", tabName, fieldsSql, keyFild);
            }
            using (IDbConnection dbConnection = GetConnection())
            {
                return dbConnection.Execute(sql, paras);
            }
        }

        private string[] GetFieldsFromDictionary(Dictionary<string, object> keyValues, string keyFild = "")
        {
            var result = new List<string>();
            foreach (var entry in keyValues)
            {
                if (entry.Key != keyFild)
                {
                    result.Add(entry.Key);
                }
            }
            return result.ToArray();
        }


        void FormartSqlToSortAndPage(GridData grid, ref string sql, ref string countSql, ref WhereBuilder builder)
        {
            sql = "";
            countSql = "";
            if (!sql.StartsWith("select", StringComparison.CurrentCultureIgnoreCase))
            {
                //sql = string.Format("SELECT * FROM(SELECT row=ROW_NUMBER() OVER(ORDER BY {0} {1}),* FROM {2}", grid.SortField, grid.SortDirection, builder.FromSql); //
                sql = "select * from " + builder.FromSql;                       //仅支持SQL Server 2012及以上
                countSql = "select count(*) from " + builder.FromSql;
            }
            if (builder.Wheres.Count > 0)
            {
                string strWhere = " where " + String.Join(" and ", builder.Wheres);
                sql += strWhere;
                countSql += strWhere;
            }
            //sql += ") AS A WHERE row between @PageStartIndex AND @PageSize";
            #region 仅支持SQL Server 2012及以上
            sql += " order by " + grid.SortField + " " + grid.SortDirection;
            sql += " OFFSET @PageStartIndex ROWS FETCH NEXT @PageSize ROWS ONLY";
            builder.Parameters.Add("PageSize", grid.PageSize);
            builder.Parameters.Add("PageStartIndex", grid.PageSize * (grid.PageIndex - 1));
            #endregion
            //builder.Parameters.Add("PageSize", grid.PageSize * grid.PageIndex);
            //builder.Parameters.Add("PageStartIndex", grid.PageSize * (grid.PageIndex - 1) + 1);
        }
    }
}
