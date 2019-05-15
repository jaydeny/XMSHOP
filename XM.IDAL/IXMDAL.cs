using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.IDAL
{
    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-5-9
    /// 修改时间：2019-
    /// 功能：对接game项目
    /// </summary>
    public interface IXMDAL
    {

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-9
        /// 修改时间：2019-
        /// 功能：会员充值积分时,先检查余额
        /// </summary>
        decimal CheckRamainder(Dictionary<string,object> paras);


        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-14
        /// 修改时间：2019-
        /// 功能：如果余额足够,就进行充值
        /// </summary>
        int GameRecharge(Dictionary<string, object> paras);


        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-14
        /// 修改时间：2019-
        /// 功能：反充值
        /// </summary>
        int ShopRecharge(Dictionary<string, object> paras);
    }
}
