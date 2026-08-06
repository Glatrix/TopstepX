namespace TopstepX.Models.Contracts
{
    public class SearchContractResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public List<ContractModel>? contracts { get; set; }
    }
}
