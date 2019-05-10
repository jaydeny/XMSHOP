using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IGoodsDAL
    {
        /// <summary>
        /// 查询所有商品
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        IEnumerable<T> QryGoods<T>(Dictionary<string, object> paras, out int iCount);

        /// <summary>
        /// 查询商品资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GoodsEntity QryGoodsInfo(string id );
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteGoods(string id);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);

    }
}
