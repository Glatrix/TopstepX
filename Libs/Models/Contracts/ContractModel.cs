namespace TopstepX.Models.Contracts
{
    public class ContractModel
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public double tickSize { get; set; }
        public double tickValue { get; set; }
        public bool activeContract { get; set; }
        public string symbolId { get; set; } = string.Empty;
    }
}
