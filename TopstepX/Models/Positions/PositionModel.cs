namespace TopstepX.Models.Positions
{
    public class PositionModel
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public string contractId { get; set; } = string.Empty;
        public string? contractDisplayName { get; set; }
        public DateTime creationTimestamp { get; set; }
        public PositionType type { get; set; }
        public int size { get; set; }
        public decimal averagePrice { get; set; }
    }
}
