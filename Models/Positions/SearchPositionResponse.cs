namespace TopstepX.Models.Positions
{
    public class SearchPositionResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public List<PositionModel>? positions { get; set; }
    }
}
