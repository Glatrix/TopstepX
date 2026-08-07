namespace TopstepX.Models.Gateway
{
    public class GatewayUserAccount
    {
        public int id { get; set; }
        public string name { get; set; }
        public double balance { get; set; }
        public bool canTrade { get; set; }
        public bool isVisible { get; set; }
        public bool simulated { get; set; }
    }
}
