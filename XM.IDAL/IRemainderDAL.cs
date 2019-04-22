using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IRemainderDAL
    {
        bool EditRemainder(RemainderEntity remainder);
        IEnumerable<T> QryRemainder<T>(Dictionary<string, object> paras, out int iCount);
        int Save(Dictionary<string, object> paras);
    }
}
