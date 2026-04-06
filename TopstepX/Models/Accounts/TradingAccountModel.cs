namespace TopstepX.Models.Accounts
{
    public class TradingAccountModel
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public decimal balance { get; set; }
        public bool canTrade { get; set; }
        public bool isVisible { get; set; }
        public bool simulated { get; set; }
    }
}