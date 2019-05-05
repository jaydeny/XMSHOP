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
    public class UserDAL : BaseDal,IUserDAL
    {
        /// <summary>
        /// 根据用户id获取用户
        /// </summary>
        public UserEntity GetUserByUserId(string userId)
        {
            const string sql = "select top 1 * from v_user_list where id = @UserId";
            return QuerySingle<UserEntity>(sql, new { UserId = userId });
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        public UserEntity GetUserById(string id)
        {
            string sql = "select * from v_user_list where id = @ID";
            return QuerySingle<UserEntity>(sql, new { ID = id });
        }

        public UserEntity GetUserByAccountName(string name)
        {
            string sql = "select * from v_user_list where UserAccountName=@AccountName";
            return QuerySingle<UserEntity>(sql, new { AccountName = name });
        }

        /// <summary>
        /// 首次登陆强制修改密码
        /// </summary>
        public bool InitUserPwd(UserEntity user)
        {
            string sql = "update tbUser set [user_pwd] = @UserPwd where id = @ID";
            return Execute(sql, new { UserPwd = user.UserPassword, ID = user.id }) > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public bool ChangePwd(UserEntity user)
        {
            string sql = "update tbUser set user_pwd = @UserPwd,status_id=1 where id = @Id";
            SqlParameter[] paras = {
                                   new SqlParameter("@UserPwd",user.UserPassword),
                                   new SqlParameter("@Id",user.id)
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
        public UserEntity UserLogin(string loginId, string loginPwd)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select top 1 id,user_AN,[user_pwd],user_email,user_mp,user_CBY,user_CDT,role_id,status_id from tbUser ");
            sbSql.Append("where user_AN=@UserId and user_pwd=@UserPwd");
            SqlParameter[] paras = {
                                       new SqlParameter("@UserId",loginId),
                                       new SqlParameter("@UserPwd",loginPwd)
                                       };
            UserEntity user = null;
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sbSql.ToString(), paras);
            if (dt.Rows.Count > 0)
            {
                user = new UserEntity();

                if (!DBNull.Value.Equals(dt.Rows[0]["id"]))
                    user.id = int.Parse(dt.Rows[0]["id"].ToString());
                if (!DBNull.Value.Equals(dt.Rows[0]["user_AN"]))
                    user.UserAccountName = dt.Rows[0]["user_AN"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["user_pwd"]))
                    user.UserPassword = dt.Rows[0]["user_pwd"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["user_mp"]))
                    user.UserMobliePhone = dt.Rows[0]["user_mp"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["user_email"]))
                    user.UserEmail = dt.Rows[0]["user_email"].ToString();
                if (!DBNull.Value.Equals(dt.Rows[0]["status_id"]))
                    user.StatusID = int.Parse(dt.Rows[0]["status_id"].ToString());
                if (!DBNull.Value.Equals(dt.Rows[0]["role_id"]))
                    user.RoleID = int.Parse(dt.Rows[0]["role_id"].ToString());
                return user;
            }
            return user;
        }

        /// <summary>
        /// 根据用户id判断用户是否可用
        /// </summary>
        public UserEntity CheckLoginByUserId(string userId)
        {
            string sql = "select top 1 id,user_AN,[user_pwd],user_email,user_mp,user_CBY,user_CDT,role_id from tbUser where AccountName=@UserId and status_id=1";
            UserEntity user = null;
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sql, new SqlParameter("@UserId", userId));
            if (dt.Rows.Count > 0)
            {
                user = new UserEntity();
                DataRowToModel(user, dt.Rows[0]);
                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public int AddUser(UserEntity user)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbUser(user_AN,[user_pwd],user_email,user_mp,user_CBY,user_CDT,role_id,status_id)");
            strSql.Append(" values ");
            strSql.Append("(@UserAcountName,@UserPassword,@UserMobliePhone,@UserEmail,@UserCreateBy,@UserCreateDate,@RoleID,@StatusID)");
            strSql.Append(";SELECT @@IDENTITY");   //返回插入用户的主键
            SqlParameter[] paras = {
                                   new SqlParameter("@UserAcountName",user.UserAccountName),
                                   new SqlParameter("@UserPassword",user.UserPassword),
                                   new SqlParameter("@UserMobliePhone",user.UserMobliePhone),
                                   new SqlParameter("@UserEmail",user.UserEmail),
                                   new SqlParameter("@UserCreateBy",user.UserCreateBy),
                                   new SqlParameter("@UserCreateDate",user.UserCreateDate),
                                   new SqlParameter("@RoleID",user.RoleID),
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
            list.Add("delete from tbUser where id in (" + idList + ")");

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
        public bool EditUser(UserEntity user)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbUser set ");
            strSql.Append("user_AN=@AccountName,user_mp=@MobilePhone,user_email=@Email,status_id=@StatusID,role_id=@RoleID");
            strSql.Append(" where id=@Id");

            SqlParameter[] paras = {
                                   new SqlParameter("@UserAcountName",user.UserAccountName),
                                   new SqlParameter("@UserMobliePhone",user.UserMobliePhone),
                                   new SqlParameter("@Email",user.UserEmail),
                                   new SqlParameter("@StatusID",user.StatusID),
                                   new SqlParameter("@RoleID",user.RoleID),
                                   new SqlParameter("@Id",user.id),
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
            strSql.Append("select u.user_AN,u.user_CBY,r.role_name from tbUser u");
            strSql.Append(" left join tbRole r on u.role_id = r.id");
            strSql.Append(" where u.id = @userId");
            return SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, strSql.ToString(), new SqlParameter("@userId", userId));
        }

        /// <summary>
        /// 把DataRow行转成实体类对象
        /// </summary>
        private void DataRowToModel(UserEntity model, DataRow dr)
        {
            if (!DBNull.Value.Equals(dr["id"]))
                model.id = int.Parse(dr["id"].ToString());

            if (!DBNull.Value.Equals(dr["user_AN"]))
                model.UserAccountName = dr["user_AN"].ToString();

            if (!DBNull.Value.Equals(dr["user_pwd"]))
                model.UserPassword = dr["user_pwd"].ToString();


            if (!DBNull.Value.Equals(dr["user_mp"]))
                model.UserMobliePhone = dr["user_mp"].ToString();

            if (!DBNull.Value.Equals(dr["user_email"]))
                model.UserEmail = dr["user_email"].ToString();

            if (!DBNull.Value.Equals(dr["user_CBY"]))
                model.UserCreateBy = dr["user_CBY"].ToString();

            if (!DBNull.Value.Equals(dr["user_CDT"]))
                model.UserCreateDate = Convert.ToDateTime(dr["user_CDT"]);

            if (!DBNull.Value.Equals(dr["role_id"]))
                model.RoleID = int.Parse(dr["role_id"].ToString());

            if (!DBNull.Value.Equals(dr["status_id"]))
                model.StatusID = int.Parse(dr["status_id"].ToString());


        }

        public IEnumerable<T> QryUsers<T>(Dictionary<string, object> paras, out int iCount)
        {
            iCount = 0;
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "v_user_list";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "userAn", "UserAccountName", "LIKE", "'%'+@userAn+'%'");
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
            return QuerySingle<T>("SELECT * FROM v_user_list WHERE id=@ID", paras, CommandType.Text);
        }

        /// <summary>
        /// 检查账号、邮箱是否存在重复
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int CheckUseridAndEmail(Dictionary<string, object> paras)
        {
            return QuerySingle<int>("P_User_CheckUseridAndEmail", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 新增/修改用户
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int Save(Dictionary<string, object> paras)
        {
            return StandardInsertOrUpdate("tbUser", paras);
        }
    }
}
