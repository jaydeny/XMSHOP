﻿/*-------------------------------------*
 * 创建人:         梁钧淋
 * 创建时间:       2019/06/03
 * 最后修改时间:    
 * 最后修改原因:
 * 修改历史:
 * 2019/06/03       梁钧淋       创建
 *-------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    /// <summary>
    /// 功能: 活动类
    /// </summary>
    public class ActivityDAL : BaseDal, IActivityDAL
    {
        /// <summary>
        /// 功能:获取当前登录会员可以参与的活动
        /// </summary>
        /// <typeparam name="ActivityEntity"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<ActivityEntity> QryAC<ActivityEntity>(Dictionary <string, object> param)
        {
            string sql = "select * from tbActivity where (Receiver is null or Receiver ='@agent_AN' or  Receiver like '%@agent_AN%') and StartDate < @Date and EndDate > @Date";
            
            IEnumerable<ActivityEntity> list = QueryList<ActivityEntity>(sql, param);
            List<ActivityEntity> result = list.ToList();
            return result ;
        }

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
            builder.AddWhereAndParameter(paras, "Title");
            builder.AddWhereAndParameter(paras, "id");
            return SortAndPage<T>(builder, grid, out iCount);
        }
        /// <summary>
        /// 获取折扣活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<CustomDisEntity> GetDisByTag(int id)
        {
            string sql = "SELECT * FROM tbCustomDis WHERE Ac_id = @id";
            return QueryList<CustomDisEntity>(sql, new { id });
        }
        /// <summary>
        /// 获取满减活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<CustomFullEntity> GetfullByTag(int id)
        {
            string sql = "SELECT * FROM tbCustomFull WHERE Ac_id = @id";
            return QueryList<CustomFullEntity>(sql, new { id });
        }

        /// <summary>
        /// 功能:查询活动的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActivityEntity ActivityEntity<ActivityEntity>(int Ac_id)
        {
            string sql = "select * from tbActivity where id = @Ac_id";
            return QuerySingle<ActivityEntity>(sql, new { Ac_id });
        }

        /// <summary>
        /// 功能:查询单一活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public CustomFullEntity QryACTypeInfoFull<CustomFullEntity>(int Ac_id)
        {
            string sql = "select * from tbCustomFull where Ac_id = @Ac_id";
            return QuerySingle<CustomFullEntity>(sql, new { Ac_id});
        }

        /// <summary>
        /// 功能:查询单一活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public CustomDisEntity QryACTypeInfoDis<CustomDisEntity>(int Ac_id)
        {
            string sql = "select * from tbCustomDis where Ac_id = @Ac_id";
            return QuerySingle<CustomDisEntity>(sql, new { Ac_id });
        }

        /// <summary>
        /// 功能:满赠类型活动的订单记录和奖励发放
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int RecordAcInfo(Dictionary<string, object> param)
        {
            return QuerySingle<int>("P_tbReceiveAward_RecordAcInfo",param, CommandType.StoredProcedure);
        }
    }
}
