using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class ActivityDAL : BaseDal, IActivityDAL
    {
        public IEnumerable<ActivityEntity> QryAC<ActivityEntity>(Dictionary<string,object> param)
        {
            string sql = "select * from tbActivity where startDate < @Date and endDate > @Date and receiver == @agent_AN or receiver == null";
            IEnumerable<ActivityEntity> list = QueryList<ActivityEntity>(sql,param);
            return list;
        }
    }
}
