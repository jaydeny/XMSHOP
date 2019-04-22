using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IRechargeDAL
    {
        int AddRecharge(RechargeEntity recharge);
        IEnumerable<T> QryRecharge<T>(Dictionary<string, object> paras, out int iCount);
        int Save(Dictionary<string, object> paras);
    }
}
