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
        public AgentEntity GetUserByUserId(string userId)
        {
            const string sql = "select top 1 agent_id,agent_AN,[agent_pwd],agent_email,agent_mp,agent_CBY,agent_CDT,status_id from tbAgent where agent_id = @UserId";
            return QuerySingle<AgentEntity>(sql, new { UserId = userId });
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        public AgentEntity GetUserById(string id)
        {
            string sql = "select agent_id,agent_AN,[agent_pwd],agent_email,agent_mp,agent_CBY,agent_CDT,status_id from tbAgent where agent_id = @ID";
            return QuerySingle<AgentEntity>(sql, new { ID = id });
        }

        /// <summary>
        /// 首次登陆强制修改密码
        /// </summary>
        public bool InitUserPwd(AgentEntity user)
        {
            string sql = "update tbVip set [agent_pwd] = @UserPwd where agent_id = @ID";
            return Execute(sql, new { UserPwd = user.AgentPassword, ID = user.AgentID }) > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public bool ChangePwd(AgentEntity user)
        {
            string sql = "update tbAgent set agent_pwd = @UserPwd,status_id=1 where agent_id = @Id";
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
            sbSql.Append("select top 1 agent_id,agent_AN,[agent_pwd],agent_email,agent_mp,agent_CBY,agent_CDT,status_id from tbAgent ");
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

                if (!DBNull.Value.Equals(dt.Rows[0]["agent_id"]))
                    user.AgentID = int.Parse(dt.Rows[0]["agent_id"].ToString());
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
            string sql = "select top 1 agent_id,agent_AN,[agent_pwd],agent_email,agent_mp,agent_CBY,agent_CDT,status_id from tbVip where agent_AN=@UserId";
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
            list.Add("delete from tbAgent where agent_id in (" + idList + ")");

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
            strSql.Append(" where agent_id=@Id");

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
            strSql.Append(" where agent_id = @userId");
            return SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, strSql.ToString(), new SqlParameter("@userId", userId));
        }

        /// <summary>
        /// 把DataRow行转成实体类对象
        /// </summary>
        private void DataRowToModel(AgentEntity model, DataRow dr)
        {
            if (!DBNull.Value.Equals(dr["agent_id"]))
                model.AgentID = int.Parse(dr["agent_id"].ToString());

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
            builder.AddWhereAndParameter(paras, "userid", "agent_AN", "LIKE", "'%'+@userid+'%'");
            //builder.AddWhereAndParameter(paras, "username", "RealName", "LIKE", "'%'+@username+'%'");
            //builder.AddWhereAndParameter(paras, "IsAble");
            //builder.AddWhereAndParameter(paras, "IfChangePwd");
            //builder.AddWhereAndParameter(paras, "RoleID");
            //builder.AddWhereAndParameter(paras, "adddatestart", "CreateTime", ">");
            //builder.AddWhereAndParameter(paras, "adddateend", "CreateTime", "<");
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
            return QuerySingle<T>("SELECT * FROM v_agent_info WHERE agent_id=@ID", paras, CommandType.Text);
        }

        /// <summary>
        /// 检查账号、邮箱是否存在重复
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int CheckUseridAndEmail(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_agent_CheckUseridAndEmail", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 新增/修改用户
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbAgent", paras);
        }
    }
}
