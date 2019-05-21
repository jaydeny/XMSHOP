﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Comm;

namespace XM.Model
{
    public class NoticEntity
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        public string _id { get; set; } = Guid.NewGuid().To16String();
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime starttime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endtime { get; set; }
        /// <summary>
        /// 接收方
        /// </summary>
        public List<string> receiver { get; set; }
    }
}