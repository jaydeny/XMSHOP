using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.IDAL
{
    /// <summary>
    ///<history> 
    ///<design> 
    ///<name>梁钧淋</name> 
    ///<date>2019/5/27</date> 
    ///<description>活动的基础功能接口 </description> 
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
    public interface IActivityDAL
    {
        string GetAllActivity(Dictionary<string, object> paras, out int iCount);
       
    }
}
