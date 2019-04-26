using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class GoodsDAL : BaseDal, IGoodsDAL
    {
        public int AddGoods(GoodsEntity goods)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbGoods (goods_name,goods_intro,goods_CP,goods_CBY,goods_CDT,goods_pic,type_id)");
            strSql.Append("values");
            strSql.Append("(@GoodsName,@GoodsIntro,@Goodsprice,@CreateBy,@CreateDateTime,@Picture,@TypeId)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@GoodsName",goods.GoodsName),
                new SqlParameter("@GoodsIntro",goods.GoodsIntro),
                new SqlParameter("@Goodsprice",goods.GoodsPrice),
                new SqlParameter("@CreateBy",goods.GoodsCreateBy),
                new SqlParameter("@CreateDateTime",goods.GoodsCreateTime),
                new SqlParameter("@Picture",goods.GoodsPicture),
                new SqlParameter("@TypeId",goods.GoodsType)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));

        }

        public bool DeleteGoods(string id)
        {
            List<string> list = new List<string>();
            list.Add("delete from tbGoods where id in (" + id + ")");
            try
            {
                int count = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, list);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EditGoods(GoodsEntity goods)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbGoods set");
            strSql.Append("goods_name=@GoodsName,goods_intro=@GoodsIntro,goods_CP=@Goodsprice,goods_CBY=@CreateBy,goodsCDT=@CreateDateTime,goods_pic=@Picture,type_id=@TypeId");
            strSql.Append("where id = @GoodsId");
            SqlParameter[] paras =
            {
                new SqlParameter("@GoodsName",goods.GoodsName),
                new SqlParameter("@GoodsIntro",goods.GoodsIntro),
                new SqlParameter("@Goodsprice",goods.GoodsPrice),
                new SqlParameter("@CreateBy",goods.GoodsCreateBy),
                new SqlParameter("@CreateDateTime",goods.GoodsCreateTime),
                new SqlParameter("@Picture",goods.GoodsPicture),
                new SqlParameter("@TypeId",goods.GoodsType),
                new SqlParameter("@GoodsId",goods.GoodsID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
            
        }
        public IEnumerable<T> QryGoods<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_goods_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "goods_name", "GoodsName", "LIKE", "'%'+@goods_name+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        public T QryGoodsInfo<T>(Dictionary<string, object> paras)
        {
            throw new NotImplementedException();
        }

        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbGoods", paras);
            
        }
    }
}
