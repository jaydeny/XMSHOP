using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IAgentGoodsDAL
    {
        //查询所有商品
        IEnumerable<T> QryGoods<T>(Dictionary<string, object> paras, out int iCount);
        //查询商品资料
        T QryGoodsInfo<T>(Dictionary<string, object> paras);

        //添加商品
        int AddGoods(AgentGoodsEntity goods);
        //删除商品
        bool DeleteGoods(AgentGoodsEntity goods);
        //修改商品
        bool EditGoods(AgentGoodsEntity goods);
        //保存
        int Save(Dictionary<string, object> paras);
    }
}
