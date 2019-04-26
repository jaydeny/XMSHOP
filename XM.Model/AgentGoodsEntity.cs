using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class AgentGoodsEntity
    {
        public int AgentGoodsID { get; set; }
        public int GoodsID { get; set; }
        public int StatusID { get; set; }
        public decimal Price { get; set; }
        public DateTime UPTime { get; set; }
        public string AgentAccountName { get; set; }
        public string GoodsName { get; set; }
    }
}
