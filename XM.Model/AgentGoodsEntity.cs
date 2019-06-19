using System;

namespace XM.Model
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 代理商品类
    /// </summary>
    public class AgentGoodsEntity
    {
        /// <summary>
        /// 代理商品编号
        /// 主键
        /// </summary>
        public int AgentGoodsID { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int GoodsID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int StatusID { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime UPTime { get; set; }
        /// <summary>
        /// 代理账号
        /// </summary>
        public string AgentAccountName { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }
    }
}
