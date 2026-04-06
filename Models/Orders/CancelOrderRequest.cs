namespace TopstepX.Models.Orders
{
    public class CancelOrderRequest
    {
        public int accountId { get; set; }
        public long orderId { get; set; }
    }
}
