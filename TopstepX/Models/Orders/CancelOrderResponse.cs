namespace TopstepX.Models.Orders
{
    public class CancelOrderResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
    }
}
