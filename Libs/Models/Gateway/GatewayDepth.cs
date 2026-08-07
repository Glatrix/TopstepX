namespace TopstepX.Models.Gateway
{
    public class GatewayDepth
    {
        public string timestamp { get; set; }
        public DomType type { get; set; }
        public double price { get; set; }
        public double volume { get; set; }
        public int currentVolume { get; set; }
    }
}
