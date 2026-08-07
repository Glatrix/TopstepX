namespace TopstepX.Models.Orders
{
    public class PlaceOrderRequest
    {
        public int accountId { get; set; }
        public string contractId { get; set; } = string.Empty;
        public OrderType type { get; set; }
        public OrderSide side { get; set; }
        public int size { get; set; }
        public double? limitPrice { get; set; }
        public double? stopPrice { get; set; }
        public double? trailPrice { get; set; }
        public string? customTag { get; set; }
        public PlaceOrderBracket? stopLossBracket { get; set; }
        public PlaceOrderBracket? takeProfitBracket { get; set; }
    }
}
