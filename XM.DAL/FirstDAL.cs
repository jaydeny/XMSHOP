﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.DAL.comm;
using XM.IDAL;

namespace XM.DAL
{
    public class FirstDAL : BaseDal, IFirstDAL
    {
        public IEnumerable<T> GetStore<T>(Dictionary<string, object> paras)
        {
            return QueryList<T>("P_Select_Month", paras, CommandType.StoredProcedure);
        }
    }
}