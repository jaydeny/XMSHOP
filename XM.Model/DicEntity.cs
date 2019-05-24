using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    ///  字典参数
    ///  创建人: zxy
    ///  创建时间: 2019年5月23日
    /// </summary>
    public class DicEntity
    {
        /// <summary>
        ///  编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        ///  名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///  标识[0标识名称;1用户状态;2商品状态;3订单状态;4充值状态;5商品类型;……]
        /// </summary>
        public int tag { get; set; }

        /// <summary>
        ///  排序
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 用以关联多语系
        /// </summary>
        public string code { get; set; }
    }
}
