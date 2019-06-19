namespace XM.Model
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/22
    /// 地址类
    /// </summary>
    public class AddressEntity
    {
        /// <summary>
        /// 地址编号
        /// 主键
        /// </summary>
        public int AddressID { get; set; }
        /// <summary>
        /// 地址名称
        /// </summary>
        public string AddressName { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int VipID { get; set; }
        /// <summary>
        /// 地址状态
        /// </summary>
        public int StatusID { get; set; }
    }
}
