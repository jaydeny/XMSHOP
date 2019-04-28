using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    /// 角色菜单类
    /// 创建人：朱茂琛
    /// 创建时间：2019/4/28 
    /// </summary>
    public class RoleMenuEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 新增
        /// </summary>
        public int RmAdd { get; set; }
        /// <summary>
        /// 修改
        /// </summary>
        public int RmUpdate { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public int RmDelete { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public int RmOther { get; set; }
    }
}
