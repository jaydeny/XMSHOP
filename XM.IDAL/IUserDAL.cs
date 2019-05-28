 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IUserDAL
    {
        /// <summary>
        /// 根据用户id获取用户
        /// </summary>
        UserEntity GetUserByUserId(string userId);


        UserEntity GetUserByAccountName(string name);

        /// <summary>
        /// 首次登陆强制修改密码
        /// </summary>
        bool InitUserPwd(UserEntity user);

        /// <summary>
        /// 修改密码
        /// </summary>
        bool ChangePwd(UserEntity user);

        /// <summary>
        /// 用户登录
        /// </summary>
        UserEntity UserLogin(string loginId, string loginPwd);
        
        /// <summary>
        /// 删除用户（可批量删除，删除用户同时删除对应的：角色/权限/部门）
        /// </summary>
        bool DeleteUser(string idList);
        
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        IEnumerable<T> QryUsers<T>(Dictionary<string, object> paras, out int iCount);

        /// <summary>
        /// 查询用户资料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        T QryUserInfo<T>(Dictionary<string, object> paras);

        /// <summary>
        /// 检查账号、邮箱是否存在重复
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int CheckUseridAndEmail(Dictionary<string, object> paras);

        /// <summary>
        /// 新增/修改用户
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);


        /// <summary>
        ///  判断邮箱是否重复
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        bool JudgeEmail(int id, string userEmail);


    }
}
