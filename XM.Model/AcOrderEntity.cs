using System;

namespace XM.Model
{
    public class AcOrderEntity
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 活动id
        /// </summary>
        public int Ac_id { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public int Order_id { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Agoods_id { get; set; }
        /// <summary>
        /// vipAN
        /// </summary>
        public string Vip_AN { get; set; }
        /// <summary>
        /// 代理商AN
        /// </summary>
        public string Agent_AN { get; set; }
        /// <summary>
        /// 消费积分
        /// </summary>
        public decimal Integral { get; set; }
        /// <summary>
        /// 消费日期
        /// </summary>
        public DateTime Date { get; set; }
    }

}
