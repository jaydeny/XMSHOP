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
        /// 用户登录
        /// </summary>
        VipEntity UserLogin(string loginId, string loginPwd);
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
        /// 功能:充值记录
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        string QryRecharge(Dictionary<string, object> paras, out int iCount);
        ///作者:曾贤鑫

        #region _Signin
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
        #endregion

        #region _Login
        /// <summary>
        /// 查询vip数据以登录
        /// owen
        /// </summary>
        /// <typeparam name="VIPEntity">vip</typeparam>
        /// <param name="paras">参数:登入名,密码</param>
        /// <returns>返回一个对象,指vip</returns>
        T QryVipToLogin<T>(Dictionary<string, object> paras);
        #endregion

        #region _Recharge
        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int Recharge(Dictionary<string, object> paras);

        /// <summary>
        /// 插入余额表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int InsertRemainder(Dictionary<string, object> paras);


        #endregion

        #region _Shop
        /// <summary>
        /// 检查余额,购物
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>
        /// 0:成功
        /// 1:余额不足
        /// 2:事务出错
        /// </returns>
        int Buy(Dictionary<string, object> paras);

        /// <summary>
        /// 检查余额,购物
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>
        /// 0:成功
        /// 1:余额不足
        /// 2:事务出错
        /// </returns>
        int BuyWithAc(Dictionary<string, object> paras);
        #endregion

        #region _VipInfo
        /// <summary>
        /// 查询个人信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        T QryVipInfo<T>(Dictionary<string, object> paras);

        /// <summary>
        /// 查询原始密码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        string QryOrgPwd(Dictionary<string, object> paras);

        /// <summary>
        /// 查询vip邮箱
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        T QryVipEmail<T>(Dictionary<string, object> paras);

        /// <summary>
        /// 查询收货地址
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="icound"></param>
        /// <returns></returns>
        string QryVipAddress(Dictionary<string, object> paras, out int icound);

        /// <summary>
        /// 查询用户余额
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        decimal QryRemainder(Dictionary<string, object> paras);
        #endregion

        #region _Address
        /// <summary>
        /// 添加/修改地址
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>
        /// 0:添加
        /// 1:修改
        /// 2:报错
        /// </returns>
        int SaveAddress(Dictionary<string, object> paras);

        /// <summary>
        /// 查询地址和手机号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        int QryAdd<T>(Dictionary<string, object> paras);

        /// <summary>
        /// 查询地址和手机号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        /// <returns></returns>
        T QryTOPAdd<T>(Dictionary<string, object> paras);

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int DeleteAddress(Dictionary<string, object> paras);

        /// <summary>
        /// 功能:查询所有地址
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        List<AddressEntity> QryAllAdd(Dictionary<string,object> paras);
        #endregion

        #region _自定义
        /// <summary>
        /// 添加和修改vip共用的方法,区别在于id是否为0
        /// owen
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        int saveVIP(Dictionary<string, object> paras);

        /// <summary>
        /// 查询所有的会员
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        string QryAllVIP(Dictionary<string, object> paras, out int iCount);

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-4-28
        /// 修改时间：2019-
        /// 功能：查询代理商AN
        /// </summary>
        string QryAgentANByID(Dictionary<string, object> paras);

        /// <summary>
        /// 作者：曾贤鑫
        /// 创建时间:2019-5-16
        /// 修改时间：2019-
        /// 功能：新用户赠送10积分
        /// </summary>
        int NweVIP(Dictionary<string, object> paras);
        #endregion

        #region _商品详情
        /// <summary>
        /// 功能:查询商品详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AgoodsDTO QryAgoodsDetail(int id);
        #endregion

    }
}
