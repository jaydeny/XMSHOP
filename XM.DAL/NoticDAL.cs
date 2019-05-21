using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.DAL.comm;
using XM.IDAL;

namespace XM.DAL
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-5-21
    /// 修改时间：2019-
    /// 功能：公告的DAL
    /// </summary>
    public class NoticDAL : BaseDal, INoticDAL
    {
        /// <summary>
        /// 功能:获取所有的代理商
        /// </summary>
        /// <returns></returns>
        public string GetAllAgent()
        {
            var s = Query("select Agent_AN from tbagent", null);

            return JsonConvert.SerializeObject(new { rows = s });
        }
    }
}
