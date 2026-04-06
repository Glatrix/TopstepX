namespace TopstepX.Models.Gateway
{
    public class GatewayDepth
    {
        public string timestamp { get; set; }
        public DomType type { get; set; }
        public decimal price { get; set; }
        public decimal volume { get; set; }
        public int currentVolume { get; set; }
    }
}
