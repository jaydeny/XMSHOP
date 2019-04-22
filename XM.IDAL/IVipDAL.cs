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
        int checkANandMBandEmail(Dictionary<string, object> paras);

        /// <summary>
        /// 添加和修改vip共用的方法,区别在于id是否为0
        /// owen
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int saveVIP(Dictionary<string, object> paras);

        /// <summary>
        /// 查询vip数据以登录
        /// owen
        /// </summary>
        /// <typeparam name="VIPEntity">vip</typeparam>
        /// <param name="paras">参数:登入名,密码</param>
        /// <returns>返回一个对象,指vip</returns>
        T QryVipToLogin<T>(Dictionary<string, object> paras);

        /// <summary>
        /// 查询所有的会员
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        string QryAllVIP(Dictionary<string, object> paras, out int iCount);

        /// <summary>
        /// 购物
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int BuyGoods(Dictionary<string, object> paras);
    }
}
