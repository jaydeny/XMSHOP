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
    /// 商品类
    /// </summary>
    public class GoodsEntity
    {
        /// <summary>
        /// 商品编号
        /// 主键
        /// </summary>
        public int GoodsID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string GoodsIntro { get; set; }
        /// <summary>
        /// 商品参考价格
        /// </summary>
        public decimal GoodsPrice { get; set; }
        /// <summary>
        /// 商品创建人
        /// </summary>
        public string GoodsCreateBy { get; set; }
        /// <summary>
        /// 商品创建时间
        /// </summary>
        public DateTime GoodsCreateTime { get; set; }
        /// <summary>
        /// 商品图片保存地址
        /// </summary>
        public string GoodsPicture { get; set; }
        /// <summary>
        /// 商品类型
        /// </summary>
        public int GoodsType { get; set; }
    }
}
