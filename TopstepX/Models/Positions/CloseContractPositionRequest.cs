namespace TopstepX.Models.Positions
{
    public class CloseContractPositionRequest
    {
        public int accountId { get; set; }
        public string contractId { get; set; } = string.Empty;
    }
}
