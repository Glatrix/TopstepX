namespace TopstepX.Models.Trades
{
    public class SearchTradeRequest
    {
        public int accountId { get; set; }
        public DateTime? startTimestamp { get; set; }
        public DateTime? endTimestamp { get; set; }
    }
}
