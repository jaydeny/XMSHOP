using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class AgoodsDTO
    {
        /// <summary>
        /// 代理商品编号
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string goods_name { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string goods_intro { get; set; }
        /// <summary>
        /// 商品图片保存地址
        /// </summary>
        public string goods_pic { get; set; }
    }
}
