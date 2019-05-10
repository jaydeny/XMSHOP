using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.DAL;
using XM.IDAL;

namespace XMSHOP.TEST
{
    [TestClass]
    public class AgentDALTest
    {
        IAgentDAL agent = new AgentDAL();

        [TestMethod]
        public void QryDayTotal()
        {

            int monthDay = DateTime.DaysInMonth(2019, 5);

            string startDay = "7";
            string endDay = "8";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("year", DateTime.Now.Year.ToString());
            param.Add("month", DateTime.Now.Month.ToString());
            param.Add("startDay", startDay);
            param.Add("endDay", endDay);
            param.Add("agent_AN", "agent");
            string result = agent.QryDayTotal(param);
        }
    }
}
