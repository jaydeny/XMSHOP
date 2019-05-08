using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IGoodsTypeDAL
    {
        int AddType(GoodsTypeEntity goodsType);
        bool DeleteType(string id);
        bool EditType(GoodsTypeEntity goodsType);
        IEnumerable<T> QryType<T>(Dictionary<string, object> paras, out int iCount);
        int Save(Dictionary<string, object> paras);
        GoodsTypeEntity GetTypeById(string id);
    }
}
