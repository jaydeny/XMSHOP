using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    /// 角色类
    /// 创建人：朱茂琛
    /// 创建时间：2019/4/28
    /// </summary>
    public class RoleEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标识码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
