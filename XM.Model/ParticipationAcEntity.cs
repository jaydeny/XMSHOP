using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class ParticipationAcEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? _id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary> 
        public string Vip_AN { get; set; }

        /// <summary>
        /// 活动ID
        /// </summary>
        public int? ActID { get; set; }

        /// <summary>
        /// 目标积分
        /// </summary>
        public int? ActTarget { get; set; }

        /// <summary>
        /// 当前的积分
        /// </summary>
        public int? PresentNow { get; set; }

        /// <summary>
        /// 总次数
        /// </summary>
        public int? Total { get; set; } = -1;

        /// <summary>
        /// 当前次数
        /// </summary>
        public int? PresentCount { get; set; }
    }
}
