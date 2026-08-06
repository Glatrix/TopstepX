using TopstepX.Models.Positions;


namespace TopstepX.Models.Gateway
{
    public class GatewayUserPosition
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public string contractId { get; set; }
        public string creationTimestamp { get; set; }
        public PositionType type { get; set; }
        public int size { get; set; }
        public decimal averagePrice { get; set; }
    }
}
