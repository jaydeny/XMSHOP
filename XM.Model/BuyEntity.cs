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
    /// 购买记录
    /// </summary>
    public class BuyEntity
    {
        /// <summary>
        /// 购买记录编号
        /// 主键
        /// </summary>
        public int BuyID { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyTime { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyCount { get; set; }
        /// <summary>
        /// 购买账户
        /// </summary>
        public string BuyAccountName { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int GoodsID { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal BuyPrice { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderID { get; set; }
    }
}
