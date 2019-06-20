using System.Collections.Generic;

namespace XM.Model
{
    public class RecordCollect
    {
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
        public IEnumerable<GameResult> result { get; set; }
    }
}
