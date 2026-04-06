namespace TopstepX.Models.Trades
{
    public class SearchHalfTradeResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public List<HalfTradeModel>? trades { get; set; }
    }
}
