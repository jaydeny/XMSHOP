using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class ShoppCartEntity
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public int vip_ID { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int Agoods_ID { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int goods_id { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Agoods_Count { get; set; }
        /// <summary>
        /// 活动编号
        /// </summary>
        public int Ac_ID { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public int status_id { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 商品上架时间
        /// </summary>
        public DateTime up_time { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_name { get; set; }
    }
}
