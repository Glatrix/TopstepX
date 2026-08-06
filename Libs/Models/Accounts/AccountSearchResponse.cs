namespace TopstepX.Models.Accounts
{
    public class AccountSearchResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public List<TradingAccountModel>? accounts { get; set; }
    }
}