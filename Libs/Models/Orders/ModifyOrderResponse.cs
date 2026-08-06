namespace TopstepX.Models.Orders
{
    public class ModifyOrderResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
    }
}
