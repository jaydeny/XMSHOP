using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Model
{
    public class GoodsEntity
    {
        public int GoodsID { get; set; }
        public string GoodsName { get; set; }
        public string GoodsIntro { get; set; }
        public decimal GoodsPrice { get; set; }
        public string GoodsCreateBy { get; set; }
        public DateTime GoodsCreateTime { get; set; }
        public string GoodsPicture { get; set; }
        public int GoodsType { get; set; }
    }
}
