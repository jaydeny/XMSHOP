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
    /// <summary>
    /// 作者:梁钧淋
    /// 创建时间:2019/5/27
    /// 功能: 活动类
    /// </summary>
    public class ActivityDAL : BaseDal, IActivityDAL
    {
        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int AddActivity(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbActivity_addActivity", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取符合条件的所有活动类数据
        /// </summary>
        public IEnumerable<T> getAllActivity<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbActivity";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "Publisher");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public IEnumerable<ActivityEntity> QryAC<ActivityEntity>(Dictionary<string,object> param)
        {
            string sql = "select * from tbActivity where startDate < @Date and endDate > @Date and receiver == @agent_AN or receiver == null";
            IEnumerable<ActivityEntity> list = QueryList<ActivityEntity>(sql,param);
            return list;
        }
    }
}
