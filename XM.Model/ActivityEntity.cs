using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class ActivityEntity
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 活动标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createDate { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endDate { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string publisher { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int ac_type { get; set; }
    }
}
