namespace TopstepX.Models.Gateway
{
    public class GatewayTrade
    {
        public string symbolId { get; set; }
        public double price { get; set; }
        public string timestamp { get; set; }
        public TradeLogType type { get; set; }
        public double volume { get; set; }
    }
}
