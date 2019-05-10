using Newtonsoft.Json;
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
    public class AgentDAL : BaseDal,IAgentDAL
    {
        /// <summary>
        /// 根据用户id获取用户
        /// </summary>
        public AgentEntity GetUserByUserId(int userId)
        {
            
                //const string sql = "select v.*,a.AgentAccountName from v_vip_list v join v_agent_list a on v.AgentID = a.AgentID where a.AgentID = 2";
            const string sql = "select  * from v_agent_list where AgentID = @UserId";
            return QuerySingle<AgentEntity>(sql, new { UserId = userId });
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        public AgentEntity GetUserById(string id)
        {
            string sql = "select id,agent_AN,[agent_pwd],agent_email,agent_mp,agent_CBY,agent_CDT,status_id from tbAgent where id = @ID";
            return QuerySingle<AgentEntity>(sql, new { ID = id });
        }

        /// <summary>
        /// 首次登陆强制修改密码
        /// </summary>
        public bool InitUserPwd(AgentEntity user)
        {
            string sql = "update tbVip set [agent_pwd] = @UserPwd where id = @ID";
            return Execute(sql, new { UserPwd = user.AgentPassword, ID = user.AgentID }) > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public bool ChangePwd(AgentEntity user)
        {
            string sql = "update tbAgent set agent_pwd = @UserPwd,status_id=1 where id = @Id";
            SqlParameter[] paras = {
                                   new SqlParameter("@UserPwd",user.AgentPassword),
                                   new SqlParameter("@Id",user.AgentID)
                                   };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, sql, paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public AgentEntity UserLogin(string loginId, string loginPwd)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select top 1 id,agent_AN,[agent_pwd],agent_email,agent_mp,agent_CBY,agent_CDT,status_id from tbAgent ");
            sbSql.Append("where agent_AN=@UserId and agent_pwd=@UserPwd");
            SqlParameter[] paras = {
                                       new SqlParameter("@UserId",loginId),
                                       new SqlParameter("@UserPwd",loginPwd)
                                       };
            AgentEntity user = null;
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sbSql.ToString(), paras);
            if (dt.Rows.Count > 0)
            {
                user = new AgentEntity();

                if (!DBNull.Value.Equals(dt.Rows[0]["id"]))
                    user.AgentID = int.Parse(dt.Rows[0]["id"].ToString());
                if (!DBNull.Value.Equals(dt.Rows[0]["agent_AN"]))
                    user.AgentAccountName = dt.Rows[0]["agent_AN"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["agent_pwd"]))
                    user.AgentPassword = dt.Rows[0]["agent_pwd"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["agent_mp"]))
                    user.MobliePhone = dt.Rows[0]["agent_mp"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["agent_email"]))
                    user.Email = dt.Rows[0]["agent_email"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["status_id"]))
                    user.StatusID = int.Parse(dt.Rows[0]["status_id"].ToString());
                return user;
            }
            return user;
        }

        /// <summary>
        /// 根据用户id判断用户是否可用
        /// </summary>
        public AgentEntity CheckLoginByUserId(string userId)
        {
            string sql = "select top 1 id,agent_AN,[agent_pwd],agent_email,agent_mp,agent_CBY,agent_CDT,status_id from tbVip where agent_AN=@UserId";
            AgentEntity user = null;
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sql, new SqlParameter("@UserId", userId));
            if (dt.Rows.Count > 0)
            {
                user = new AgentEntity();
                DataRowToModel(user, dt.Rows[0]);
                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public int AddUser(AgentEntity user)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbAgent(agent_AN,[agent_pwd],agent_email,agent_mp,agent_CBY,agent_CDT,status_id)");
            strSql.Append(" values ");
            strSql.Append("(@UserAcountName,@UserPassword,@UserMobliePhone,@UserEmail,@UserCreateBy,@UserCreateDate,@RoleID,@StatusID)");
            strSql.Append(";SELECT @@IDENTITY");   //返回插入用户的主键
            SqlParameter[] paras = {
                                   new SqlParameter("@UserAcountName",user.AgentAccountName),
                                   new SqlParameter("@UserPassword",user.AgentPassword),
                                   new SqlParameter("@UserMobliePhone",user.MobliePhone),
                                   new SqlParameter("@UserEmail",user.Email),
                                   new SqlParameter("@UserCreateBy",user.CreateBy),
                                   new SqlParameter("@UserCreateDate",user.CreateTime),
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
            list.Add("delete from tbAgent where id in (" + idList + ")");

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
        public bool EditUser(AgentEntity user)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbAgent set ");
            strSql.Append("agent_AN=@AccountName,agent_mp=@MobilePhone,agent_email=@Email,status_id=@StatusID");
            strSql.Append(" where id=@Id");

            SqlParameter[] paras = {
                                   new SqlParameter("@UserAcountName",user.AgentAccountName),
                                   new SqlParameter("@UserMobliePhone",user.MobliePhone),
                                   new SqlParameter("@Email",user.Email),
                                   new SqlParameter("@StatusID",user.StatusID),
                                   new SqlParameter("@Id",user.AgentID),
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
            strSql.Append("select agent_AN,agent_CBY from tbUser u");
            strSql.Append(" where id = @userId");
            return SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, strSql.ToString(), new SqlParameter("@userId", userId));
        }

        /// <summary>
        /// 把DataRow行转成实体类对象
        /// </summary>
        private void DataRowToModel(AgentEntity model, DataRow dr)
        {
            if (!DBNull.Value.Equals(dr["id"]))
                model.AgentID = int.Parse(dr["id"].ToString());

            if (!DBNull.Value.Equals(dr["agent_AN"]))
                model.AgentAccountName = dr["agent_AN"].ToString();

            if (!DBNull.Value.Equals(dr["agent_pwd"]))
                model.AgentPassword = dr["agent_pwd"].ToString();


            if (!DBNull.Value.Equals(dr["agent_mp"]))
                model.MobliePhone = dr["agent_mp"].ToString();

            if (!DBNull.Value.Equals(dr["agent_email"]))
                model.Email = dr["agent_email"].ToString();

            if (!DBNull.Value.Equals(dr["agent_CBY"]))
                model.CreateBy = dr["agent_CBY"].ToString();

            if (!DBNull.Value.Equals(dr["agent_CDT"]))
                model.CreateTime = Convert.ToDateTime(dr["agent_CDT"]);

            if (!DBNull.Value.Equals(dr["status_id"]))
                model.StatusID = int.Parse(dr["status_id"].ToString());


        }

        public IEnumerable<T> QryUsers<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_agent_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "AgentName", "AgentAccountName", "LIKE", "'%'+@AgentName+'%'");
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
            return QuerySingle<T>("SELECT * FROM v_agent_list WHERE id=@ID", paras, CommandType.Text);
        }

        /// <summary>
        /// 检查账号、邮箱是否存在重复
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int CheckUseridAndEmail(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_agent_checkANandMBandEmail", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 新增/修改用户
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int Save(Dictionary<string, object> paras)
        {
            return StandarInsertOrUpdate("tbAgent", paras);
        }

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int Recharge(Dictionary<string, object> paras)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbrecharge(recharge_name,recharge_price,recharge_time,agent_id,vip_id) ");
            strSql.Append("values(recharge_name=@recharge_name, recharge_price=@recharge_price, recharge_time=@recharge_time,agent_id=@agent_id,vip_id=@vip_id )");
            
            return QuerySingle<int>( strSql.ToString(), paras, CommandType.Text);
        }

        #region _Signin
        /// <summary>
        /// 注册代理商时,检查是否有登录名,邮箱,手机重复
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
        public int CheckANandMBandEmail(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbagent_checkANandMBandEmail", paras, CommandType.StoredProcedure);
        }
        #endregion

        #region _Login
        /// <summary>
        /// 查询代理商数据以登录
        /// owen
        /// </summary>
        /// <typeparam name="VIPEntity">vip</typeparam>
        /// <param name="paras">参数:登入名,密码</param>
        /// <returns>返回一个对象,指vip</returns>
        public T QryAgentToLogin<T>(Dictionary<string, object> paras)
        {
            return QuerySingle<T>("SELECT * FROM v_agent_info WHERE AgentAccountName=@agent_AN AND AgentPassword=@agent_pwd", paras, CommandType.Text);
        }
        #endregion

        #region _Goods
        /// <summary>
        /// 上架或者修改商品
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int MakeGoods(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbagent_AgoodsInsertAndUpdate", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询所有的代理商商品
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string QryAgoods(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbAgoods a join tbgoods b on a.goods_id = b.id";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString()
            };
            builder.AddWhereAndParameter(paras, "goods_Name", "a.goods_Name", "LIKE", "'%'+@goods_Name+'%'");
            builder.AddWhereAndParameter(paras, "agent_AN");
            builder.AddWhereAndParameter(paras, "status_id");

            var s = SortAndPage(builder, grid, out iCount, "a.*,b.goods_intro,b.goods_pic");
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }

        /// <summary>
        /// 查询所有未上架的后台商品
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string QryGoods(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_goods_list a left join tbagoods b on a.GoodsID=b.goods_id";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString()
            };
            builder.AddWhereAndParameter(paras, "goods_id", "a.id", "in", "null");
            builder.AddWhereAndParameter(paras, "goods_Name", "a.GoodsName", "LIKE", "'%'+@goods_Name+'%'");

            var s = SortAndPage(builder, grid, out iCount, "a.* ");
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
        #endregion

        #region _Info
        /// <summary>
        /// 查询代理商信息
        /// owen
        /// </summary>
        /// <typeparam name="VIPEntity">vip</typeparam>
        /// <param name="paras">参数:登入名,密码</param>
        /// <returns>返回一个对象,指vip</returns>
        public string QryAgentInfo<T>(Dictionary<string, object> paras)
        {
            var objAgentInfo = QuerySingle<T>("SELECT * FROM v_agent_info WHERE AgentAccountName=@agent_AN", paras, CommandType.Text);

            string retData = JsonConvert.SerializeObject(new { rows = objAgentInfo });
            return retData;
        }
        #endregion

        #region _Form
        /// <summary>
        /// 查询所有的代理商
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns> 
        public string QryReportForm(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tborder";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString()
            };
            builder.AddWhereAndParameter(paras, "startTime", "order_date", ">", "@startTime");
            builder.AddWhereAndParameter(paras, "endTime", "order_date", "<", "@endTime");
            builder.AddWhereAndParameter(paras, "agent_AN");

            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
        #endregion

        #region _自定义
        /// <summary>
        /// 添加和修改代理商共用的方法,区别在于id是否为0
        /// owen
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int SaveAgent(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbagent", paras);
        }

        /// <summary>
        /// 查询会员,分页
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string QryAllAgent(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbagent";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString()
            };
            builder.AddWhereAndParameter(paras, "agent_AN", "agent_AN", "LIKE", "'%'+@agent_AN+'%'");
            builder.AddWhereAndParameter(paras, "agent_mp");
            builder.AddWhereAndParameter(paras, "agent_email", "agent_email", "LIKE", "'%'+@agent_email+'%'");
            builder.AddWhereAndParameter(paras, "status_id");

            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
        #endregion



    }
}
