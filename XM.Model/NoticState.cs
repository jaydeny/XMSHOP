using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Comm;

namespace XM.Model
{
    public class NoticState
    {
        public string _id { get; set; } = Guid.NewGuid().To16String();
        /// <summary>
        /// 公告ID
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 会员账号
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// 状态[1已读 2删除]
        /// </summary>
        public int state { get; set; }
    }
}
