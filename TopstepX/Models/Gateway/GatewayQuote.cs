namespace TopstepX.Models.Gateway
{
    public class GatewayQuote
    {
        public string symbol { get; set; }
        public string symbolName { get; set; }
        public decimal lastPrice { get; set; }
        public decimal bestBid { get; set; }
        public decimal bestAsk { get; set; }
        public decimal change { get; set; }
        public decimal changePercent { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public long volume { get; set; }
        public string lastUpdated { get; set; }
        public string timestamp { get; set; }
    }
}
