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
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "goods_Name", "a.goods_Name", "LIKE", "'%'+@goods_Name+'%'");
            builder.AddWhereAndParameter(paras, "agent_AN");
            builder.AddWhereAndParameter(paras, "status_id");
            builder.AddWhereAndParameter(paras, "type_id");

            var s = SortAndPage(builder, grid, out iCount, "a.*,b.goods_intro,b.goods_pic");
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }

        /// <summary>
        /// 查询所有未上架的后台商品
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string QryGoods(Dictionary<string, object> paras)
        {
            var result = Query("select a.* from v_goods_list a left join tbagoods b on a.GoodsID=b.goods_id where b.goods_id is null order by id OFFSET @pi ROWS FETCH NEXT @pageSize ROWS ONLY", paras);
            var iCount = QuerySingle<int>("select count(0)  from v_goods_list a left join tbagoods b on a.GoodsID=b.goods_id where b.goods_id is null", paras);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = result });
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
        /// 功能：查询日期,总营业额
        /// </summary>
        public string QryDayTotal(Dictionary<string, object> paras)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append("convert(varchar(10),order_date, 120) as date, SUM(order_total) as total ");
            builder.Append("from tborder ");
            builder.Append("where YEAR(order_date)= @year and  MONTH(order_date) between @startMonth and @endMonth and DAY(order_date) between @startDay and @endDay ");
            builder.Append("and agent_AN = @agent_AN ");
            builder.Append("GROUP BY convert(varchar(10),order_date, 120)");

            var s = Query(builder.ToString(), paras);

            string retData = JsonConvert.SerializeObject(new { rows = s });
            return retData;
        }
        /// <summary>
        /// 功能：查询日期内的记录
        /// </summary>
        public string QryDayForm(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tborder";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "day", "convert(varchar(10),order_date, 120)");
            builder.AddWhereAndParameter(paras, "agent_AN");
            builder.AddWhereAndParameter(paras, "vip_AN");
            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
        /// <summary>
        /// 查询每一笔订单的详细详细
        /// </summary>
        public string QryDetailOrder(Dictionary<string, object> paras)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append("a.id,a.order_date,a.vip_AN,a.order_mp,a.order_total,b.address_name,c.agoods_id,c.buy_count,c.buy_total,d.goods_name,d.goods_intro ");
            builder.Append("from ");
            builder.Append("tborder a join tbaddress b on a.order_address = b.id join tbbuy c on a.id = c.id join tbgoods d on c.agoods_id = d.id ");
            builder.Append("where a.id = @id ");

            var s = Query(builder.ToString(), paras);

            string retData = JsonConvert.SerializeObject(new { rows = s });
            return retData;
        }
        #endregion

        #region  后台使用
        /// <summary>
        /// 功能：查询日期，总营业额，代理商（后台使用方法）
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public string QryDayTotals(Dictionary<string, object> paras)
        {
            var s = QueryList<DayTotalDTO>("P_Select_DayTotal", paras, CommandType.StoredProcedure);
            string retData = JsonConvert.SerializeObject(new { rows = s });
            return retData;
        }
        public string QryDayForms(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_order_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            int pageSize = Convert.ToInt32(paras["pageSize"]);
            int page = Convert.ToInt32(paras["pi"]);
            builder.AddWhereAndParameter(paras, "day", "convert(varchar(10),OrderDate, 120)");
            builder.AddWhereAndParameter(paras, "agent_AN","AgentAccountName", "LIKE", "'%'+@agent_AN+'%'");
            builder.AddWhereAndParameter(paras, "VipAccountName", "LIKE", "'%'+@vip_AN+'%'");
            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = (int)Math.Ceiling((double)iCount / pageSize), rows = s, page = page });
            return retData;
        }
        public string QryDetailOrders(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_report_info";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "order_id", "convert(varchar(10),OrderDate, 120)");
            builder.AddWhereAndParameter(paras, "AgentAccountName", "LIKE", "'%'+@agent_AN+'%'");
            builder.AddWhereAndParameter(paras, "VipAccountName", "LIKE", "'%'+@vip_AN+'%'");
            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
        #endregion

        #region _RechargeFrom

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4/29
        /// 修改时间：2019-
        /// 功能：查询日期,总营业额
        /// </summary>
        public string QryDayRechargeTotal(Dictionary<string, object> paras)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append("convert(varchar(10),recharge_time, 120) as date, SUM(recharge_price) as total ");
            builder.Append("from tbrecharge ");
            builder.Append("where YEAR(recharge_time)= @year and MONTH(recharge_time) between @startMonth and @endMonth and DAY(recharge_time) between @startDay and @endDay ");
            //builder.Append("and agent_id = @agent_id ");
            builder.Append("GROUP BY convert(varchar(10),recharge_time, 120)");

            var s = Query(builder.ToString(), paras);

            string retData = JsonConvert.SerializeObject(new { rows = s });
            return retData;
        }

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-10
        /// 修改时间：2019-
        /// 功能：查询日期内的记录
        /// </summary>
        public string QryDayRechargeForm(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbrecharge";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            int pageSize = Convert.ToInt32(paras["pageSize"]);
            int page = Convert.ToInt32(paras["pi"]);
            builder.AddWhereAndParameter(paras, "day", "convert(varchar(10),recharge_time, 120)", "like", "@day+'%'");
            builder.AddWhereAndParameter(paras, "agent_AN");
            builder.AddWhereAndParameter(paras, "vip_AN");
            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = (int)Math.Ceiling((double)iCount / pageSize), rows = s ,page = page});
            return retData;
        }
        #endregion

        #region _Examine(会员充值审核)

        /// <summary>
        /// 作者：梁钧淋
        /// 创建时间:2019-5-23
        /// 修改时间：2019-
        /// 功能：查询时间段内的充值数据
        /// </summary>
        public string QryDayExamineTotal(Dictionary<string, object> paras)
        {

            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(" convert(varchar(10),recharge_time, 120) as date, SUM(recharge_price) as total , count(id) as count ");
            builder.Append("from tbrecharge ");
            builder.Append("where recharge_time <= @endTime and recharge_time >= @startTime ");
            builder.Append("and agent_AN = @agent_AN ");
            if (paras["status"] != null)
                builder.Append(" and status_id = @status ");
            if (paras["vip_AN"] != null)
                builder.Append(" and vip_AN = @vip_AN ");
            builder.Append("GROUP BY convert(varchar(10),recharge_time, 120)");

            var s = Query(builder.ToString(), paras);

            string retData = JsonConvert.SerializeObject(new { rows = s });
            return retData;
        }

        /// <summary>
        /// 作者：梁钧淋
        /// 创建时间:2019-5-23
        /// 修改时间：2019-
        /// 功能：查询日期内的记录
        /// </summary>
        public string QryDayExamineForm(Dictionary<string, object> paras, out int iCount)
        {
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbrecharge";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "day", "convert(varchar(10),recharge_time, 120)", "like", "@day+'%'");
            builder.AddWhereAndParameter(paras, "agent_AN");
            builder.AddWhereAndParameter(paras, "vip_AN");
            builder.AddWhereAndParameter(paras, "status_id");
            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
        /// <summary>
        /// 审核存储过程
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>
        ///     0,1 通过审核
        ///     2 审核不通过
        /// </returns>
        public int RechargeAudit(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_tbremiander_remiand", paras, CommandType.StoredProcedure);
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

        public IEnumerable<T> QryAgent<T>()
        {
            string strSql = "select * from v_agent_list";
            return QueryList<T>(strSql);
        }
        #endregion


    }
}
