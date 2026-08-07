namespace TopstepX.Models.Orders
{
    public class ModifyOrderRequest
    {
        public int accountId { get; set; }
        public long orderId { get; set; }
        public int? size { get; set; }
        public double? limitPrice { get; set; }
        public double? stopPrice { get; set; }
        public double? trailPrice { get; set; }
    }
}
