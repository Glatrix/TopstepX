namespace TopstepX.Models.Orders
{
    public class PlaceOrderResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public long orderId { get; set; }
    }
}
