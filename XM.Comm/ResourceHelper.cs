using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace XM.Comm
{

    /// <summary>
    /// 作者：曾贤鑫
    /// 创建时间:2019-06-05
    /// 修改时间：2019-
    /// 功能：返回资源文件中的内容
    /// </summary>
    public class ResourceHelper
    {
        public static string ResourcePath = "xiemiShop.XM.Comm.ReturnResource.resx";

        private static ResourceManager GetResourceManager()
        {
            ResourceManager ResourceRead = new ResourceManager(ResourcePath, typeof(CommFunc).Assembly);
            return ResourceRead;
        }

        public string GetRteurnResource(string aKey)
        {
            ResourceManager ResourceRead = GetResourceManager();
            return ResourceRead.GetString(aKey);
        }
    }
}
