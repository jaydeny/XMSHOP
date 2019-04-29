using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Model;

namespace XM.IDAL
{
    public interface IMenuDAL
    {
        
        /// <summary>
        /// 添加 菜单
        /// </summary>
        int AddMenu(MenuEntity menu);

        /// <summary>
        /// 删除 菜单
        /// </summary>
        bool DeleteMenu(string id);

        /// <summary>
        /// 修改 菜单
        /// </summary>
        bool EditMenu(MenuEntity menu);

        IEnumerable<T> GetAllMenu<T>(Dictionary<string, object> paras, out int iCount);
    }
}
