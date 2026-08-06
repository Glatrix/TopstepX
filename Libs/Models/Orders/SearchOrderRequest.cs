namespace TopstepX.Models.Orders
{
    public class SearchOrderRequest
    {
        public int accountId { get; set; }
        public DateTime startTimestamp { get; set; }
        public DateTime? endTimestamp { get; set; }
    }
}
