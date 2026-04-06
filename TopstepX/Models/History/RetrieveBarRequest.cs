namespace TopstepX.Models.History
{
    public class RetrieveBarRequest
    {
        public string contractId { get; set; } = string.Empty;
        public bool live { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public AggregateBarUnit unit { get; set; }
        public int unitNumber { get; set; }
        public int limit { get; set; }
        public bool includePartialBar { get; set; }
    }
}
