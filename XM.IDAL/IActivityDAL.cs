using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.IDAL
{
    public interface IActivityDAL
    {
        IEnumerable<ActivityEntity> QryAC<ActivityEntity>(Dictionary<string, object> param);
    }
}
