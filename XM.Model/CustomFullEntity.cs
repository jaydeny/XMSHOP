using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class CustomFullEntity
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
        /// 溢满
        /// </summary>
        public string ac_full { get; set; }
        /// <summary>
        /// 赠送积分
        /// </summary>
        public string minus { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public string times { get; set; }
    }
}
