using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IJurisdictionDAL
    {
        int AddJurisdiction(JurisdictionEntity jurisdiction);
        bool DeleteJurisdiction(string id);
        bool EditJurisdiction(JurisdictionEntity jurisdiction);
        IEnumerable<T> QryJurisdiction<T>(Dictionary<string, object> paras, out int iCount);
        int Save(Dictionary<string, object> paras);

    }
}
