using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class CustomDisEntity
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
        /// 活动类型id
        /// </summary>
        public int ac_type { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public string discount { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public string times { get; set; }
    }
}
