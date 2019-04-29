using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using XM.DAL.comm;
using XM.IDAL;
using XM.Model;

namespace XM.DAL
{
    public class VipDAL : BaseDal, IVipDAL
    {
        /// <summary>
        /// 根据用户id获取用户
        /// </summary>
        public VipEntity GetUserByUserId(string userId)
        {
            const string sql = "select top 1 * from v_vip_list where id = @UserId";
            return QuerySingle<VipEntity>(sql, new { UserId = userId });
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        public VipEntity GetUserById(string id)
        {
            string sql = "select * from v_vip_list where id = @ID";
            return QuerySingle<VipEntity>(sql, new { ID = id });
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        public VipEntity UserLogin(string loginId, string loginPwd)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select top 1 id,vip_AN,[vip_pwd],vip_email,vip_mp,vip_CDT,agent_id,status_id from tbvip ");
            sbSql.Append("where vip_AN=@UserId and vip_pwd=@UserPwd");
            SqlParameter[] paras = {
                                       new SqlParameter("@UserId",loginId),
                                       new SqlParameter("@UserPwd",loginPwd)
                                       };
            VipEntity user = null;
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sbSql.ToString(), paras);
            if (dt.Rows.Count > 0)
            {
                user = new VipEntity();

                if (!DBNull.Value.Equals(dt.Rows[0]["id"]))
                    user.VipID = int.Parse(dt.Rows[0]["id"].ToString());
                if (!DBNull.Value.Equals(dt.Rows[0]["vip_AN"]))
                    user.VipAccountName = dt.Rows[0]["vip_AN"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["vip_pwd"]))
                    user.VipPassword = dt.Rows[0]["vip_pwd"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["vip_mp"]))
                    user.VipMobliePhone = dt.Rows[0]["vip_mp"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["vip_email"]))
                    user.VipEmail = dt.Rows[0]["vip_email"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["status_id"]))
                    user.StatusID = int.Parse(dt.Rows[0]["status_id"].ToString());
                return user;
            }
            return user;
        }



        /// <summary>
        /// 添加用户
        /// </summary>
        public int AddUser(VipEntity user)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbVip(vip_AN,[vip_pwd],vip_email,vip_mp,vip_CDT,agent_id,status_id)");
            strSql.Append(" values ");
            strSql.Append("(@UserAcountName,@UserPassword,@UserMobliePhone,@UserEmail,@UserCreateBy,@UserCreateDate,@RoleID,@StatusID)");
            strSql.Append(";SELECT @@IDENTITY");   //返回插入用户的主键
            SqlParameter[] paras = {
                                   new SqlParameter("@UserAcountName",user.VipAccountName),
                                   new SqlParameter("@UserPassword",user.VipPassword),
                                   new SqlParameter("@UserMobliePhone",user.VipMobliePhone),
                                   new SqlParameter("@UserEmail",user.VipEmail),
                                   new SqlParameter("@UserCreateDate",user.CreateTime),
                                   new SqlParameter("@RoleID",user.AgentID),
                                   new SqlParameter("@StatusID",user.StatusID)
                                   };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras));
        }

        /// <summary>
        /// 删除用户（可批量删除，删除用户同时删除对应的权限和所处的部门）
        /// </summary>
        public bool DeleteUser(string idList)
        {
            List<string> list = new List<string>();
            list.Add("delete from tbvip where id in (" + idList + ")");

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

        /// <summary>
        /// 修改用户
        /// </summary>
        public bool EditUser(VipEntity user)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbvip set ");
            strSql.Append("vip_AN=@AccountName,vip_mp=@MobilePhone,vip_email=@Email,status_id=@StatusID,agent_id=@RoleID");
            strSql.Append(" where id=@Id");

            SqlParameter[] paras = { 
                                   new SqlParameter("@UserAcountName",user.VipAccountName),
                                   new SqlParameter("@UserMobliePhone",user.VipMobliePhone),
                                   new SqlParameter("@Email",user.VipEmail),
                                   new SqlParameter("@StatusID",user.StatusID),
                                   new SqlParameter("@RoleID",user.AgentID),
                                   new SqlParameter("@Id",user.VipID),
                                   };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取用户信息（“我的信息”）
        /// </summary>
        public DataTable GetUserInfo(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select vip_AN,vip_CBY from v_vip_list ");
            strSql.Append(" where id = @userId");
            return SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, strSql.ToString(), new SqlParameter("@userId", userId));
        }

        /// <summary>
        /// 把DataRow行转成实体类对象
        /// </summary>
        private void DataRowToModel(VipEntity model, DataRow dr)
        {
            if (!DBNull.Value.Equals(dr["id"]))
                model.VipID = int.Parse(dr["id"].ToString());

            if (!DBNull.Value.Equals(dr["vip_AN"]))
                model.VipAccountName = dr["vip_AN"].ToString();

            if (!DBNull.Value.Equals(dr["vip_pwd"]))
                model.VipPassword = dr["vip_pwd"].ToString();


            if (!DBNull.Value.Equals(dr["vip_mp"]))
                model.VipMobliePhone = dr["vip_mp"].ToString();

            if (!DBNull.Value.Equals(dr["vip_email"]))
                model.VipEmail = dr["vip_email"].ToString();

            if (!DBNull.Value.Equals(dr["vip_CDT"]))
                model.CreateTime = Convert.ToDateTime(dr["vip_CDT"]);

            if (!DBNull.Value.Equals(dr["agent_id"]))
                model.AgentID = int.Parse(dr["agent_id"].ToString());

            if (!DBNull.Value.Equals(dr["status_id"]))
                model.StatusID = int.Parse(dr["status_id"].ToString());


        }

        public IEnumerable<T> QryUsers<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_vip_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "vip_AN", "VipAccountName", "LIKE", "'%'+@vip_AN+'%'");
            return SortAndPage<T>(builder, grid, out iCount);
        }

        /// <summary>
        /// 查询用户资料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        public T QryUserInfo<T>(Dictionary<string, object> paras)
        {
            return QuerySingle<T>("SELECT * FROM v_user_info WHERE id=@ID", paras, CommandType.Text);
        }

        /// <summary>
        /// 检查账号、邮箱是否存在重复
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int CheckUseridAndEmail(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbvip_checkANandMBandEmail", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 新增/修改用户
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbvip", paras);
        }


        ///作者:曾贤鑫


        /// <summary>
        /// 注册vip时,检查是否有登录名,邮箱,手机重复
        /// owen
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>
        /// 返回:
        /// 0:无重复
        /// 1:AN重复
        /// 2:MB重复
        /// 3:Email重复
        /// </returns>
        public int checkANandMBandEmail(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbvip_checkANandMBandEmail", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 添加和修改vip共用的方法,区别在于id是否为0
        /// owen
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int saveVIP(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbvip", paras);
        }

        /// <summary>
        /// 查询vip数据以登录
        /// owen
        /// </summary>
        /// <typeparam name="VIPEntity">vip</typeparam>
        /// <param name="paras">参数:登入名,密码</param>
        /// <returns>返回一个对象,指vip</returns>
        public T QryVipToLogin<T>(Dictionary<string, object> paras)
        {
            return QuerySingle<T>("SELECT * FROM v_vip_info WHERE VipAccountName=@vip_AN AND VipPassword=@vip_pwd", paras, CommandType.Text);
        }

        /// <summary>
        /// 查询会员,分页
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string QryAllVIP(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbvip";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString()
            };
            builder.AddWhereAndParameter(paras, "vip_AN", "vip_AN", "LIKE", "'%'+@vip_AN+'%'");

            builder.AddWhereAndParameter(paras, "vip_mp");
            builder.AddWhereAndParameter(paras, "vip_Email", "vip_Email", "LIKE", "'%'+@vip_Email+'%'");
            builder.AddWhereAndParameter(paras, "status_id");
            builder.AddWhereAndParameter(paras, "agent_id");
            
            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int Recharge(Dictionary<string, object> paras)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbrecharge(recharge_name,recharge_price,recharge_time,agent_id,vip_id)");
            strSql.Append(" values ");
            strSql.Append("(@recharge_name, @recharge_price, @recharge_time,@agent_id,@vip_id )");
            SqlParameter[] p = {
                                   new SqlParameter("@recharge_name",paras["recharge_name"]),
                                   new SqlParameter("@recharge_price",paras["recharge_price"]),
                                   new SqlParameter("@recharge_time",paras["recharge_time"]),
                                   new SqlParameter("@agent_id",paras["agent_id"]),
                                   new SqlParameter("@vip_id",paras["vip_id"]),
                                   };
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, strSql.ToString(), p));
            
        }

        /// <summary>
        /// 插入余额表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int InsertRemainder(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbremiander_remiand", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 检查余额
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int Buy(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbvip_Shopping", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询个人信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string QryVipInfo<T>(Dictionary<string, object> paras)
        {
            var vipInfo = QuerySingle<T>("SELECT VipAccountName,VipMobliePhone,VipEmail FROM v_vip_info WHERE VipAccountName=@vip_AN", paras, CommandType.Text);

            string retData = JsonConvert.SerializeObject(new { total = 1, rows = vipInfo });

            return retData;
        }

        /// <summary>
        /// 查询原始密码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string QryOrgPwd(Dictionary<string, object> paras)
        {
            return QuerySingle<string>("SELECT pwd FROM tbvip WHERE id=@vip_id", paras, CommandType.Text);
        }

        /// <summary>
        /// 查询vip邮箱
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        public T QryVipEmail<T>(Dictionary<string, object> paras)
        {
            return QuerySingle<T>("SELECT * FROM v_vip_list WHERE VipAccountName=@vip_AN", paras, CommandType.Text);
        }

        /// <summary>
        /// 查询收货地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string QryVipAddress(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbaddress";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString()
            };
            builder.AddWhereAndParameter(paras, "vip_id");

            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }

        /// <summary>
        /// 检查余额,购物
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>
        /// 0:添加
        /// 1:修改
        /// 2:报错
        /// </returns>
        public int SaveAddress(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbaddress_address", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int DeleteAddress(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("delete tbaddress where id=@id and vip_id=@vip_id", paras, CommandType.Text);
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4-28
        /// 修改时间：2019-
        /// 功能：查询代理商AN
        /// </summary>
        public string QryAgentANByID(Dictionary<string, object> paras)
        {
            return QuerySingle<string>("select agent_AN from tbagent where id = @agent_id", paras, CommandType.Text);
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4/29
        /// 修改时间：2019-
        /// 功能：查询订单
        /// </summary>
        public string QryOrder(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tborder a join tbaddress b on a.order_address = b.id";
            GridData grid = new GridData()
            {
                PageIndex = 1,
                PageSize = 10,
                SortField = "a.id"
            };
            builder.AddWhereAndParameter(paras, "startTime", "order_date", ">", "@startTime");
            builder.AddWhereAndParameter(paras, "endTime", "order_date", "<", "@endTime");
            builder.AddWhereAndParameter(paras, "agent_AN");
            builder.AddWhereAndParameter(paras, "vip_AN");

            var s = SortAndPage(builder, grid, out iCount, "a.* , b.address_name");
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
    }
}
