using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class ReceiverAwardEntity
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public string award { get; set; }
        /// <summary>
        /// 领取人AN
        /// </summary>
        public string vip_AN { get; set; }
        /// <summary>
        /// 领取时间
        /// </summary>
        public string receiveDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status_id { get; set; }
        /// <summary>
        /// 活动id
        /// </summary>
        public int ac_id { get; set; }
    }
}
