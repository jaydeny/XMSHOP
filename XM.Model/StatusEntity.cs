using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 状态类
    /// </summary>
    public class StatusEntity
    {
        /// <summary>
        /// 状态编号
        /// 主键
        /// </summary>
        public int StatusID { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// 状态类型
        /// </summary>
        public string StatusType { get; set; }
    }
}
