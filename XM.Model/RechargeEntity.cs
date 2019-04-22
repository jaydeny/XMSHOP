using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class RechargeEntity
    {
        public int RechargeID { get; set; }
        public string RechargeName { get; set; }
        public decimal RechargePrice { get; set; }
        public DateTime RechargeTime { get; set; }
        public int AgentID { get; set; }
        public int VipID { get; set; }
    }
}
