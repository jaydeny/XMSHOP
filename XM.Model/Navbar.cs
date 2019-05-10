using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/28
    /// 选单权限类
    /// </summary>
    public class Navbar
    {
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 添加权限
        /// </summary>
        public bool RmAdd { get; set; }
        /// <summary>
        /// 修改权限
        /// </summary>
        public bool RmUpdate { get; set; }
        /// <summary>
        /// 删除权限
        /// </summary>
        public bool RmDelete { get; set; }
        /// <summary>
        /// 其他权限
        /// </summary>
        public bool RmOther { get; set; }
        /// <summary>
        /// 标题名称
        /// </summary>
        public dynamic nameOption = "Index";
    }
}
