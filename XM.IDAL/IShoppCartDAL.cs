using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IShoppCartDAL
    {
        IEnumerable<ShoppCartEntity> QryDataByVIPID(int vipID);
    }
}
