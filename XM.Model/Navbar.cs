using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class Navbar
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Id { get; set; }
        public int MenuId { get; set; }
        public bool RmAdd { get; set; }
        public bool RmUpdate { get; set; }
        public bool RmDelete { get; set; }
        public bool RmOther { get; set; }
        public dynamic nameOption = "Index";
    }
}
