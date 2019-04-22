using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IRoleDAL
    {
        int AddRole(RoleEntity role);
        bool DeleteRole(string id);
        bool EditRole(RoleEntity role);
        IEnumerable<T> QryRole<T>(Dictionary<string, object> paras, out int iCount);
        int Save(Dictionary<string, object> paras);
    }
}
