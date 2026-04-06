namespace TopstepX.Models.History
{
    public class RetrieveBarResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public List<AggregateBarModel>? bars { get; set; }
    }
}
