namespace TopstepX.Models.Contracts
{
    public class SearchContractByIdResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public ContractModel? contract { get; set; }
    }
}
