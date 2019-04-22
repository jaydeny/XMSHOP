using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class BuyEntity
    {
        public int BuyID { get; set; }
        public DateTime BuyTime { get; set; }
        public int BuyCount { get; set; }
        public string BuyAccountName { get; set; }
        public int GoodsID { get; set; }
        public decimal BuyPrice { get; set; }
        public int OrderID { get; set; }
    }
}
