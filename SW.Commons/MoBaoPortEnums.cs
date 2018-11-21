namespace SW.Port
{
    /// <summary>
    /// 订单消费类型
    /// </summary>
    public enum ConsumeType
    {
        /// <summary>
        /// 充值
        /// </summary>
        Charge = 0,
        /// <summary>
        /// 买卡
        /// </summary>
        BuyCard = 1,
        /// <summary>
        /// 通过卡密进行充值
        /// </summary>
        ChargeByCard = 11,
        /// <summary>
        /// 通过接口直接充值
        /// </summary>
        ChargeDirect = 12
    }

}
