using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IOrderDAL
    {
        int AddOrder(OrderEntity order);
        bool DeleteOrder(string id);
        bool EditOrder(OrderEntity order);
        IEnumerable<T> QryOrder<T>(Dictionary<string, object> paras, out int iCount);
        int Save(Dictionary<string, object> paras);
    }
}
