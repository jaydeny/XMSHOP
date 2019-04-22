using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IVipDAL
    {
        /// <summary>
        /// 根据用户id获取用户
        /// </summary>
        VipEntity GetUserByUserId(string userId);

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        VipEntity GetUserById(string id);



        /// <summary>
        /// 用户登录
        /// </summary>
        VipEntity UserLogin(string loginId, string loginPwd);


        /// <summary>
        /// 添加用户
        /// </summary>
        int AddUser(VipEntity user);

        /// <summary>
        /// 删除用户（可批量删除，删除用户同时删除对应的：角色/权限/部门）
        /// </summary>
        bool DeleteUser(string idList);

        /// <summary>
        /// 修改用户
        /// </summary>
        bool EditUser(VipEntity user);

        /// <summary>
        /// 获取用户信息（“我的信息”）
        /// </summary>
        DataTable GetUserInfo(int userId);

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
    }
}
