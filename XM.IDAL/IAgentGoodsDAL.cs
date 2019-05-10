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
        /// <summary>
        /// 查询所有商品
        /// </summary>
        /// <typeparam name="T">代理商品类</typeparam>
        /// <param name="paras">参数</param>
        /// <param name="iCount">输出记录数量</param>
        /// <returns></returns>
        IEnumerable<T> QryGoods<T>(Dictionary<string, object> paras, out int iCount);

        /// <summary>
        /// 查询商品资料
        /// </summary>
        /// <typeparam name="T">代理商品类</typeparam>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        T QryGoodsInfo<T>(Dictionary<string, object> paras);

        
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods">代理商品</param>
        /// <returns></returns>
        int AddGoods(AgentGoodsEntity goods);
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="goods">代理商品</param>
        /// <returns></returns>
        bool DeleteGoods(AgentGoodsEntity goods);
        
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        bool EditGoods(AgentGoodsEntity goods);
        
        /// <summary>
        /// 添加修改方法
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);
    }
}
