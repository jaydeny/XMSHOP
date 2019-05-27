using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int ac_id { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public int order_id { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int agoods_id { get; set; }
        /// <summary>
        /// vipAN
        /// </summary>
        public string vip_AN { get; set; }
        /// <summary>
        /// 代理商AN
        /// </summary>
        public string agent_AN { get; set; }
        /// <summary>
        /// 消费积分
        /// </summary>
        public decimal integral { get; set; }
        /// <summary>
        /// 消费日期
        /// </summary>
        public DateTime date { get; set; }
    }
}
