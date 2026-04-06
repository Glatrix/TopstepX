namespace TopstepX.Models.Orders
{
    public class ModifyOrderRequest
    {
        public int accountId { get; set; }
        public long orderId { get; set; }
        public int? size { get; set; }
        public decimal? limitPrice { get; set; }
        public decimal? stopPrice { get; set; }
        public decimal? trailPrice { get; set; }
    }
}
