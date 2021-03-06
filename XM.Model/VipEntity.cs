﻿
using System;

namespace XM.Model
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 会员类
    /// </summary>
    public class VipEntity 
    {
        /// <summary>
        /// 会员编号
        /// 主键
        /// </summary>
        public int VipID { get; set; }
        /// <summary>
        /// 会员账号
        /// </summary>
        public string VipAccountName { get; set; }
        /// <summary>
        /// 会员密码
        /// </summary>
        public string VipPassword { get; set; }
        /// <summary>
        /// 会员电话
        /// </summary>
        public string VipMobliePhone { get; set; }
        /// <summary>
        /// 会员邮箱地址
        /// </summary>
        public string VipEmail { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 状态编号
        /// </summary>
        public int StatusID { get; set; }
        /// <summary>
        /// 归属代理编号
        /// </summary>
        public int AgentID { get; set; }
        /// <summary>
        /// 代理账号
        /// </summary>
        public string AgentAccountName { get; set; }
        public decimal Remainder { get; set; }
    }
}
