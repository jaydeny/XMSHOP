using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Comm;

namespace XM.Model
{
    public class ParticipationAcEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string _id { get; set; } = Guid.NewGuid().To16String();

        /// <summary>
        /// 用户名
        /// </summary> 
        public string Vip_AN { get; set; }

        /// <summary>
        /// 活动ID
        /// </summary>
        public int? Ac_id { get; set; }

        /// <summary>
        /// 目标积分
        /// </summary>
        public int? Integral_Target { get; set; }

        /// <summary>
        /// 当前的积分
        /// </summary>
        public int? Integral_now { get; set; }

        /// <summary>
        /// 总次数
        /// </summary>
        public int? Times { get; set; } = -1;

        /// <summary>
        /// 当前次数
        /// </summary>
        public int? Times_now { get; set; }
    }
}
