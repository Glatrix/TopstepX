namespace TopstepX.Models.Contracts
{
    public class ListAvailableContractResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public List<ContractModel>? contracts { get; set; }
    }
}
