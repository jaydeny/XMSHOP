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
    ///<history> 
    ///<design> 
    ///<name>梁钧淋</name> 
    ///<date>2019/5/27</date> 
    ///<description>活动的功能实现类 </description> 
    ///</design> 
    ///<edit> 
    ///<name></name>
    ///<date></date> 
    ///<description></description> 
    ///</edit> 
    ///<remarks> 
    ///</remarks> 
    ///</history> 
    /// </summary>
    public class ActivityDAL : BaseDal, IActivityDAL
    {
        /// <summary>
        /// 获取当前后台管理者的活动发布信息
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public string GetAllActivity(Dictionary<string, object> paras,out int iCount)
        {
            //var s = Query("select id ,title,content,createDate,startDate,endDate,receiver,publisher,ac_type from tbActivity where publisher = @name", paras);

            //return JsonConvert.SerializeObject(new { rows = s });
            WhereBuilder builder = new WhereBuilder();
            builder.FromSql = "tbActivity";
            GridData grid = new GridData()
            {
                PageIndex = Convert.ToInt32(paras["pi"]),
                PageSize = Convert.ToInt32(paras["pageSize"]),
                SortField = paras["sort"].ToString(),
                SortDirection = paras["order"].ToString()
            };
            builder.AddWhereAndParameter(paras, "publisher");
            var s = SortAndPage(builder, grid, out iCount);
            string retData = JsonConvert.SerializeObject(new { total = iCount, rows = s });
            return retData;
        }
    }
}
