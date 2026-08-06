namespace TopstepX.Models.Orders
{
    public class OrderModel
    {
        public long id { get; set; }
        public int accountId { get; set; }
        public string contractId { get; set; } = string.Empty;
        public string symbolId { get; set; } = string.Empty;
        public DateTime creationTimestamp { get; set; }
        public DateTime? updateTimestamp { get; set; }
        public OrderStatus status { get; set; }
        public OrderType type { get; set; }
        public OrderSide side { get; set; }
        public int size { get; set; }
        public decimal? limitPrice { get; set; }
        public decimal? stopPrice { get; set; }
        public int fillVolume { get; set; }
        public decimal? filledPrice { get; set; }
        public string? customTag { get; set; }
    }
}
