using System.Collections.Generic;
using XM.Model;

namespace XM.IDAL
{
    public interface IRoleDAL
    {
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteRole(string id);
        /// <summary>
        /// 查询角色
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        IEnumerable<T> QryRole<T>(Dictionary<string, object> paras, out int iCount);
        /// <summary>
        /// 添加和修改角色
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);
        /// <summary>
        /// 通过id查询角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleEntity GetRoleById(string id);
        IEnumerable<T> QryRole<T>();
    }
}
