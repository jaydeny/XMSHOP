﻿using System.Collections.Generic;
using XM.Model;

namespace XM.IDAL
{
    public interface IMenuDAL
    {

        /// <summary>
        /// 删除 菜单
        /// </summary>
        bool DeleteMenu(string id);
        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="paras">参数</param>
        /// <param name="iCount">输出参数</param>
        /// <returns></returns>
        IEnumerable<T> GetAllMenu<T>(Dictionary<string, object> paras, out int iCount);
        /// <summary>
        /// 添加或修改菜单
        /// </summary>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        int Save(Dictionary<string, object> paras);
        List<MenuEntity> GetAllMenuByIds(List<int> Ids);
        /// <summary>
        /// 通过Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        MenuEntity GetMenuById(string Id);
        IEnumerable<T> QryAllMenu<T>();

    }
}
