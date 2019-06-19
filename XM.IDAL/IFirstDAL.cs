using System.Collections.Generic;

namespace XM.IDAL
{
    public interface IFirstDAL
    {
        IEnumerable<T>GetStore<T>(Dictionary<string,object>paras);
    }
}
