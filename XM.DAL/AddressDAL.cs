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
    public class AddressDAL : BaseDal, IAddressDAL
    {
        public int AddAddre(AddressEntity address)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbAddress (address_name,vip_id)");
            strSql.Append("values");
            strSql.Append("(@AddressName,@VipID)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] paras =
            {
                new SqlParameter("@AddressName",address.AddressName),
                new SqlParameter("@VipID",address.VipID)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        public bool DeleteAddre(string id)
        {
            List<string> list = new List<string>();
            list.Add("delete from tbaddress where id in (" + id + ")");
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

        public bool EditAddre(AddressEntity address)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  tbAddress set");
            strSql.Append("address_name=@AddressName,vip_id@VipID");
            strSql.Append("where id=@AddressID");
            SqlParameter[] paras =
            {
                new SqlParameter("@AddressName",address.AddressName),
                new SqlParameter("@VipID",address.VipID),
                new SqlParameter("@AddressID",address.AddressID)
            };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<T> QryAddre<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbadress";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "AdressName", "address_name", "LIKE", "'%'+@AdressName+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }
        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbAddress", paras);
        }
    }
}
