using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class RecordCollect
    {
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
        public IEnumerable<GameResult> result { get; set; }
    }
}
