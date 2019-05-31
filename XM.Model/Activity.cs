using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    ///<history> 
    ///<design> 
    ///<name>梁钧淋</name> 
    ///<date>2019/5/27</date> 
    ///<description>活动的基础载体类 </description> 
    ///</design> 
    ///<edit> 
    ///<name></name>
    ///<date></date> 
    ///<description></description> 
    ///</edit> 
    ///<remarks> 
    ///</remarks> 
    ///</history> 
    /// </summary>
    public class ActivityEntity
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 活动标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime startTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime endTime { get; set; }
        /// <summary>
        /// 活动接收方[代理]
        /// </summary>
        public List<string> receiver { get; set; }
        /// <summary>
        /// 活动接收方[会员]
        /// </summary>
        public List<string> receiverMember { get; set; }
        /// <summary>
        /// 活动发布人
        /// </summary>
        public string publisher { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public string typeActivity { get; set; }
    }
}
