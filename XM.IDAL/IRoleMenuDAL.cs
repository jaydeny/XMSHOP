using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.IDAL
{
    public interface IRoleMenuDAL
    {
        /// <summary>
        /// 查询选单权限表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        IEnumerable<T> QryAllRoleMenu<T>(Dictionary<string, object> paras);
    }
}
