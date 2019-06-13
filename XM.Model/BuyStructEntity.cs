using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class BuyStructEntity
    {
        private int addressID;
        
        private string orderTotal;
        private int count;
        private int proID;
        private string proTotal;
        private int acID;

        public int AddressID { get => addressID; set => addressID = value; }
        
        public string OrderTotal { get => orderTotal; set => orderTotal = value; }
        public int Count { get => count; set => count = value; }
        public int ProID { get => proID; set => proID = value; }
        public string ProTotal { get => proTotal; set => proTotal = value; }
        public int AcID { get => acID; set => acID = value; }
    }
}
