using TopstepX.Models.Orders;


namespace TopstepX.Models.Gateway
{
    public class GatewayUserTrade
    {
        public long id { get; set; }
        public int accountId { get; set; }
        public string contractId { get; set; }
        public string creationTimestamp { get; set; }
        public decimal price { get; set; }
        public decimal profitAndLoss { get; set; }
        public decimal fees { get; set; }
        public OrderSide side { get; set; }
        public int size { get; set; }
        public bool voided { get; set; }
        public long orderId { get; set; }
    }
}
