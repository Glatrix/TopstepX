namespace TopstepX.Models.Orders
{
    public class SearchOrderResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public List<OrderModel>? orders { get; set; }
    }
}
