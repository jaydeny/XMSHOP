using System.Collections.Generic;

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

        public List<RoleMemu> roleMemus { get; set; }
    }

    public class RoleMemu
    {
        /// <summary>
        /// 选单ID
        /// </summary>
        public int m_id { get; set; }
        /// <summary>
        ///  添加
        /// </summary>
        public bool add { get; set; }
        /// <summary>
        ///  修改
        /// </summary>
        public bool update { get; set; }
        /// <summary>
        ///  删除
        /// </summary>
        public bool delete { get; set; }
        /// <summary>
        ///  其他
        /// </summary>
        public bool other { get; set; }

    }
}
