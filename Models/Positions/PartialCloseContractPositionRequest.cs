namespace TopstepX.Models.Positions
{
    public class PartialCloseContractPositionRequest
    {
        public int accountId { get; set; }
        public string contractId { get; set; } = string.Empty;
        public int size { get; set; }
    }
}
