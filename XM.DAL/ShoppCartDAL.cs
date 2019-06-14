using System.Collections;
using System.Collections.Generic;
using System.Data;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class ShoppCartDAL : BaseDal , IShoppCartDAL
    {
        /// <summary>
        /// 编辑购物车项
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int EditCart(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbShoppCart_editCart", paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 根据用户ID获取购物车
        /// </summary>
        /// <param name="vipID"></param>
        /// <returns></returns>
        public IEnumerable<ShoppCartEntity> QryDataByVIPID(int vipID)
        {
            string sql = " select sc.item_ID,sc.vip_ID,sc.Agoods_ID,sc.Agoods_Count,agoods.price GoodsPrice, agoods.up_time GoodsCreateTime, agoods.goods_name GoodsName,goods.goods_intro GoodsIntro,goods.goods_pic GoodsPicture, goods.type_id GoodsType from tbAgoods agoods join tbgoods goods on  agoods.goods_id  = goods.id join tbShoppCart sc on sc.Agoods_ID = agoods.id where sc.vip_ID = @id";
            return QueryList<ShoppCartEntity>(sql, new { id = vipID });
        }



































        /// <summary>
        /// 功能:添加购物车
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int AddCart(Dictionary<string, object> param)
        {
            string sql = "insert into tbShoppCart values(@vip_ID,@Agoods_ID,@Agoods_Count,@Ac_ID)";
            return Execute(sql, param);
        }

        
    }
}
