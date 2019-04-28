using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.IDAL
{
    public interface IRoleMenuDAL
    {
        string QryAllRoleMenu(Dictionary<string, object> paras, out int iCount);
    }
}
