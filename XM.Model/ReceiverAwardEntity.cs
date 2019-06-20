namespace XM.Model
{
    public class ReceiverAwardEntity
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public string Award { get; set; }
        /// <summary>
        /// 领取人AN
        /// </summary>
        public string Vip_AN { get; set; }
        /// <summary>
        /// 领取时间
        /// </summary>
        public string ReceiveDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status_id { get; set; }
        /// <summary>
        /// 活动id
        /// </summary>
        public int Ac_id { get; set; }
    }
}
