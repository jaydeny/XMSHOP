using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IAddressDAL
    {
        int AddAddre(AddressEntity address);
        bool DeleteAddre(string id);
        bool EditAddre(AddressEntity address);
        IEnumerable<T> QryAddre<T>(Dictionary<string, object> paras, out int iCount);
        int Save(Dictionary<string, object> paras);
    }
}
