using FrameWork.MongoDB.MongoDbConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    [Mongo("TestDB1", "Products")]
    public class LogEntity : MongoEntity
    {
        public string Operator { get; set; }
        public string Method { get; set; }
        public string boo { get; set; }
        public string reason { get; set; }
        public DateTime Time { get; set; }

    }
}
