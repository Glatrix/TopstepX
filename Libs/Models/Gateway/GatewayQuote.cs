namespace TopstepX.Models.Gateway
{
    public class GatewayQuote
    {
        public string symbol { get; set; }
        public string symbolName { get; set; }
        public double lastPrice { get; set; }
        public double bestBid { get; set; }
        public double bestAsk { get; set; }
        public double change { get; set; }
        public double changePercent { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public long volume { get; set; }
        public string lastUpdated { get; set; }
        public string timestamp { get; set; }
    }
}
