﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Comm;
using XM.IDAL;
using XM.Model;
using YMOA.MongoDB;

namespace XM.DALFactory
{
    public class DALCore
    {
        private static DALCore singleInstance;
        #region public static DALCore LoadAssamblyType<T>()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DALCore GetInstance()
        {
            if (singleInstance == null)
            {
                singleInstance = new DALCore();
            }
            return singleInstance;
        }
        #endregion

        #region internal static T LoadAssamblyType<T>(string _type)
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullType"></param>
        /// <returns></returns>
        internal static T LoadAssamblyType<T>(string _type) where T : class
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["DataAccess"];
            if (string.IsNullOrEmpty(configName))
            {
                throw new InvalidOperationException();    //抛错，代码不会向下执行了
            }
            return LoadAssamblyType<T>(configName, _type);

        }
        #endregion

        #region internal static T LoadAssamblyType<T>(string fullType)
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyName"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        internal static T LoadAssamblyType<T>(string assemblyName, string _type) where T : class
        {

            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(assemblyName);

            if (assembly != null)
            {
                Type loadType = assembly.GetType(assemblyName + "." + _type);

                if (loadType != null)
                {
                    return (T)Activator.CreateInstance(loadType);
                }
            }

            return default(T);
        }
        #endregion




        public IUserDAL User
        {
            get { return LoadAssamblyType<IUserDAL>("UserDAL"); }
        }

        public IAgentDAL Agent
        {
            get { return LoadAssamblyType<IAgentDAL>("AgentDAL"); }
        }

        public IVipDAL Vip
        {
            get { return LoadAssamblyType<IVipDAL>("VipDAL"); }
        }

        public IGoodsDAL Goods
        {
            get { return LoadAssamblyType<IGoodsDAL>("GoodsDAL"); }
        }
        public IGoodsTypeDAL Type
        {
            get { return LoadAssamblyType<IGoodsTypeDAL>("GoodsTypeDAL"); }
        }
        public IOrderDAL Order
        {
            get { return LoadAssamblyType<IOrderDAL>("OrderDAL"); }
        }
        public IRechargeDAL Recharge
        {
            get { return LoadAssamblyType<IRechargeDAL>("RechargeDAL"); }
        }
        public IRoleDAL Role
        {
            get { return LoadAssamblyType<IRoleDAL>("RoleDAL"); }
        }
        public IJurisdictionDAL Jurisdiction
        {
            get { return LoadAssamblyType<IJurisdictionDAL>("JurisdictionDAL"); }
        }
        public IRoleMenuDAL RoleMenu
        {
            get { return LoadAssamblyType<IRoleMenuDAL>("RoleMenuDAL"); }
        }
        public IMenuDAL Menu
        {
            get { return LoadAssamblyType<IMenuDAL>("MenuDAL"); }
        }
        public IDicDAL Dic
        {
            get { return LoadAssamblyType<IDicDAL>("DicDAL"); }
        }

        public IXMDAL Xm
        {
            get { return LoadAssamblyType<IXMDAL>("XMDAL"); }
        }

        public MongoDbService MDbS
        {
            get { return new MongoDbService(); }
        }

        public GameUtil Game
        {
            get { return new GameUtil(); }
        }

        public INoticDAL Notic
        {
            get { return LoadAssamblyType<INoticDAL>("NoticDAL"); }
        }
        public IActivityDAL Activity
        {
            get { return LoadAssamblyType<IActivityDAL>("ActivityDAL"); }
        }
        public IFirstDAL First
        {
            get { return LoadAssamblyType<IFirstDAL>("FirstDAL"); }
        }
        public IShoppCartDAL ShoppCart
        {
            get { return LoadAssamblyType<IShoppCartDAL>("ShoppCartDAL"); }
        }

    }
}
