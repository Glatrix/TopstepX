namespace TopstepX.Models.Gateway
{
    public class GatewayTrade
    {
        public string symbolId { get; set; }
        public decimal price { get; set; }
        public string timestamp { get; set; }
        public TradeLogType type { get; set; }
        public decimal volume { get; set; }
    }
}
