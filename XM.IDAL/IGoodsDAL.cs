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
        //查询所有商品
        IEnumerable<T> QryGoods<T>(Dictionary<string, object> paras, out int iCount);
        //查询商品资料
        GoodsEntity QryGoodsInfo(string id );

        //添加商品
        int AddGoods(GoodsEntity goods);
        //删除商品
        bool DeleteGoods(string id);
        //修改商品
        bool EditGoods(GoodsEntity goods);
        //保存
        int Save(Dictionary<string, object> paras);

    }
}
