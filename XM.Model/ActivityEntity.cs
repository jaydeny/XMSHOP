using System;

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
        public string Title { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int Ac_type { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int status_id { get; set; }
    }
}
