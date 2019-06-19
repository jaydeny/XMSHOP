namespace XM.Model
{
    /// <summary>
    /// 购买商品的载体类
    /// </summary>
    public class BuyStructEntity
    {
        private int addressID;
        
        private string orderTotal;
        private int count;
        private int proID;
        private string proTotal;
        private int acID;
        /// <summary>
        /// 地址ID
        /// </summary>
        public int AddressID { get => addressID; set => addressID = value; }
        /// <summary>
        /// 总价
        /// </summary>
        public string OrderTotal { get => orderTotal; set => orderTotal = value; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Count { get => count; set => count = value; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProID { get => proID; set => proID = value; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public string ProTotal { get => proTotal; set => proTotal = value; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int AcID { get => acID; set => acID = value; }
    }
}
