using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class VipInfoDTO
    {
        public string VipAccountName { get; set; }
        public string VipMobliePhone { get; set; }
        public string VipEmail { get; set; }
        public int StatusID { get; set; }
        public int AgentID { get; set; }
        public decimal Remainder { get; set; }
        public string AddressID { get; set; }
    }
}
