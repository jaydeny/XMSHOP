using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class AgentEntity
    {
        public int AgentID { get; set; }
        public string AgentAccountName { get; set; }
        public string AgentPassword { get; set; }
        public string MobliePhone { get; set; }
        public string Email { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int StatusID { get; set; }
    }
}
