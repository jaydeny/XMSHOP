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
    /// 充值类
    /// </summary>
    public class RechargeEntity
    {
        /// <summary>
        /// 充值编号
        /// 主键
        /// </summary>
        public int RechargeID { get; set; }
        /// <summary>
        /// 充值流水号
        /// </summary>
        public string RechargeName { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal RechargePrice { get; set; }
        /// <summary>
        /// 充值日期
        /// </summary>
        public DateTime RechargeTime { get; set; }
        /// <summary>
        /// 审核代理商编号
        /// </summary>
        public int AgentID { get; set; }
        /// <summary>
        /// 充值编号
        /// </summary>
        public int VipID { get; set; }
        public int StatusID { get; set; }
    }
}
