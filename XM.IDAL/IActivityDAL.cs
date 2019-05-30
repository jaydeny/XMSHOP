using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    /// <summary>
    /// 作者:梁钧淋
    /// 创建时间:2019/5/27
    /// 功能: 活动接口
    /// </summary>
    public interface IActivityDAL
    {
        /// <summary>
        /// 功能:获取当前登录会员可以参与的活动
        /// </summary>
        /// <typeparam name="ActivityEntity"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        List<ActivityEntity> QryAC<ActivityEntity>(Dictionary<string, object> param);

        /// <summary>
        /// 功能:查询活动的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        ActivityEntity ActivityEntity<ActivityEntity>(int Ac_id);

        /// <summary>
        /// 功能:查询单一活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        CustomFullEntity QryACTypeInfoFull<CustomFullEntity>(int Ac_id);

        /// <summary>
        /// 功能:查询单一活动的类型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        CustomDisEntity QryACTypeInfoDis<CustomDisEntity>(int Ac_id);

        /// <summary>
        /// 功能:满赠类型活动的订单记录和奖励发放
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int RecordAcInfo(Dictionary<string, object> param);
        IEnumerable<ActivityEntity> QryAC<ActivityEntity>(Dictionary<string, object> param);
        /// <summary>
        /// 获取符合条件的所有活动类数据
        /// </summary>
        /// <typeparam name="T">ActivityEntity</typeparam>
        /// <param name="paras">字典参数</param>
        /// <param name="iCount">数据库中符合条件的条数</param>
        /// <returns>ActivityEntity(活动集合类)</returns>
        IEnumerable<T> getAllActivity<T>(Dictionary<string, object> paras, out int iCount);
        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="paras">参数集</param>
        /// <returns>
        ///  0  满额活动
        ///  1  折扣活动
        ///  2  添加不成功
        /// </returns>
        int AddActivity(Dictionary<string, object> paras);
        /// <summary>
        ///  根据活动ID获取详细信息
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <returns>返回折扣活动详细信息</returns>
        IEnumerable<CustomDisEntity> GetDisByTag(int id);

        /// <summary>
        ///  根据活动ID获取详细信息
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <returns>返回满减活动详细信息</returns>
        IEnumerable<CustomFullEntity> GetfullByTag(int id);

    }
}
