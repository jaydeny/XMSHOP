using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class OrderEntity
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int AddressID { get; set; }
        public string OrderMP { get; set; }
        public string VipAccountName { get; set; }
        public string AgentAccountName { get; set; }
        public decimal OrderPrice { get; set; }
    }
}
