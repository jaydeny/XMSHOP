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
    /// 代理商类
    /// </summary>
    public class AgentEntity
    {
        /// <summary>
        /// 代理编号
        /// 主键
        /// </summary>
        public int AgentID { get; set; }
        /// <summary>
        /// 代理账号
        /// </summary>
        public string AgentAccountName { get; set; }
        /// <summary>
        /// 代理密码
        /// </summary>
        public string AgentPassword { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string MobliePhone { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int StatusID { get; set; }
    }
}
