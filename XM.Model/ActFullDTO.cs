using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class ActFullDTO
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int Ac_type { get; set; }
        /// <summary>
        /// 溢满
        /// </summary>
        public string Ac_full { get; set; }
        /// <summary>
        /// 赠送积分
        /// </summary>
        public string Minus { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public string Times { get; set; }
    }
}
