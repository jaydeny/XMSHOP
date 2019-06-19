using System;

namespace XM.Model
{
    public class ShoppCartEntity
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public int item_ID { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int vip_ID { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int Agoods_ID { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Agoods_Count { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GoodsPrice { get; set; }
        /// <summary>
        /// 商品上架时间
        /// </summary>
        public DateTime GoodsCreateTime { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsIntro { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string GoodsPicture { get; set; }
        /// <summary>
        /// 商品类型
        /// </summary>
        public int GoodsType { get; set; }

    }
}
