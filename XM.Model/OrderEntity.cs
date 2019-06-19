using System;

namespace XM.Model
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 订单类
    /// </summary>
    public class OrderEntity
    {
        /// <summary>
        /// 订单编号
        /// 主键
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 送货地址编号
        /// </summary>
        public int AddressID { get; set; }
        /// <summary>
        /// 收件人手机号
        /// </summary>
        public string OrderMP { get; set; }
        /// <summary>
        /// 下单账号
        /// </summary>
        public string VipAccountName { get; set; }
        /// <summary>
        /// 代理账号
        /// </summary>
        public string AgentAccountName { get; set; }
        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal OrderPrice { get; set; }
    }
}
