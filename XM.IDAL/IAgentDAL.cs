using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IAgentDAL
    {
        /// <summary>
        /// 根据用户id获取用户
        /// </summary>
        AgentEntity GetUserByUserId(int userId);

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        AgentEntity GetUserById(string id);

        /// <summary>
        /// 首次登陆强制修改密码
        /// </summary>
        bool InitUserPwd(AgentEntity user);

        /// <summary>
        /// 修改密码
        /// </summary>
        bool ChangePwd(AgentEntity user);

        /// <summary>
        /// 用户登录
        /// </summary>
        AgentEntity UserLogin(string loginId, string loginPwd);

        /// <summary>
        /// 根据用户id判断用户是否可用
        /// </summary>
        AgentEntity CheckLoginByUserId(string userId);

        /// <summary>
        /// 添加用户
        /// </summary>
        int AddUser(AgentEntity user);

        /// <summary>
        /// 删除用户（可批量删除，删除用户同时删除对应的：角色/权限/部门）
        /// </summary>
        bool DeleteUser(string idList);

        /// <summary>
        /// 修改用户
        /// </summary>
        bool EditUser(AgentEntity user);

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
        /// 会员充值
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Recharge(Dictionary<string, object> paras);


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
        int CheckANandMBandEmail(Dictionary<string, object> paras);
        #endregion

        #region _Login
        /// <summary>
        /// 查询代理商数据以登录
        /// owen
        /// </summary>
        /// <typeparam name="VIPEntity">vip</typeparam>
        /// <param name="paras">参数:登入名,密码</param>
        /// <returns>返回一个对象,指vip</returns>
        T QryAgentToLogin<T>(Dictionary<string, object> paras);
        #endregion

        #region _Goods
        /// <summary>
        /// 代理商给商品定价或者修改状态
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>
        /// 0:添加或者修改成功
        /// 1:该代理商,已存在此商品
        /// 
        /// </returns>
        int MakeGoods(Dictionary<string, object> paras);

        /// <summary>
        /// 查询所有的代理商商品
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        string QryAgoods(Dictionary<string, object> paras, out int iCount);

        /// <summary>
        /// 查询所有未上架的后台商品
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        string QryGoods(Dictionary<string, object> paras, out int iCount);
        #endregion

        #region _Info
        /// <summary>
        /// 查询代理商信息
        /// owen
        /// </summary>
        /// <typeparam name="VIPEntity">vip</typeparam>
        /// <param name="paras">参数:登入名,密码</param>
        /// <returns>返回一个对象,指vip</returns>
        string QryAgentInfo<T>(Dictionary<string, object> paras);
        #endregion

        #region _From
        /// <summary>
        /// 查询所有的代理商
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        string QryReportForm(Dictionary<string, object> paras, out int iCount);
        #endregion

        #region 自定义
        /// <summary>
        /// 添加和修改代理商共用的方法,区别在于id是否为0
        /// owen
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int SaveAgent(Dictionary<string, object> paras);

        /// <summary>
        /// 查询所有的代理商
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        string QryAllAgent(Dictionary<string, object> paras, out int iCount);
        
        #endregion
    }
}
