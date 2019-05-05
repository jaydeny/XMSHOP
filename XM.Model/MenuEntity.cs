using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    /// <summary>
    /// 创建者：朱茂琛
    /// 创建时间：2019/4/28
    /// 菜单类
    /// </summary>
    public class MenuEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 导航菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级节点id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单标识码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortValue { get; set; }
    }
}
