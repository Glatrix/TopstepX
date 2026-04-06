using TopstepX.Models.Orders;


namespace TopstepX.Models.Gateway
{
    public class GatewayUserOrder
    {
        public long id { get; set; }
        public int accountId { get; set; }
        public string contractId { get; set; }
        public string symbolId { get; set; }
        public string creationTimestamp { get; set; }
        public string updateTimestamp { get; set; }
        public OrderStatus status { get; set; }
        public OrderType type { get; set; }
        public OrderSide side { get; set; }
        public int size { get; set; }
        public decimal? limitPrice { get; set; }
        public decimal? stopPrice { get; set; }
        public int fillVolume { get; set; }
        public decimal? filledPrice { get; set; }
        public string customTag { get; set; }
    }
}
