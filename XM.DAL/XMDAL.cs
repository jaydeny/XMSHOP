using System.Collections.Generic;
using System.Data;
using XM.DAL.comm;
using XM.IDAL;

namespace XM.DAL
{
    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-5-9
    /// 修改时间：2019-
    /// 功能：对接game项目
    /// </summary>
    /// 
    public class XMDAL : BaseDal, IXMDAL
    {

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-9
        /// 修改时间：2019-
        /// 功能：会员充值积分是,先检查余额
        /// </summary>
        public decimal CheckRamainder(Dictionary<string, object> paras)
        {
            return QuerySingle<decimal>("SELECT Remainder FROM v_vip_remainder WHERE VipAccountName=@vip_AN", paras, CommandType.Text);
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-14
        /// 修改时间：2019-
        /// 功能：如果余额足够,就进行充值
        /// </summary>
        public int GameRecharge(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_game_Recharge", paras, CommandType.StoredProcedure);
        }
    }
}
