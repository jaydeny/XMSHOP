using System;

namespace XM.Model
{
    public class AgentInfoDTO
    {
        public int AgentID { get; set; }
        public string AgentAccountName { get; set; }
        public string MobliePhone { get; set; }
        public string Email { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int StatusID { get; set; }
    }
}
