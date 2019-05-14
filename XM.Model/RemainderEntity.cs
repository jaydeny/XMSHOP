using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 余额类
    /// </summary>
    public class RemainderEntity
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Remainder { get; set; }
    }
}
