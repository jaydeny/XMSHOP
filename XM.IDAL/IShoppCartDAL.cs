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
        /// <summary>
        /// 根据ID获取购物车信息
        /// </summary>
        /// <param name="vipID"></param>
        /// <returns></returns>
        IEnumerable<ShoppCartEntity> QryDataByVIPID(int vipID);
      /// <summary>
      /// 编辑购物车项
      /// </summary>
      /// <param name="paras"></param>
      /// <returns></returns>
       int EditCart(Dictionary<string, object> paras);

































        /// <summary>
        /// 功能:添加购物车
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int AddCart(Dictionary<string, object> param);
    }
}
