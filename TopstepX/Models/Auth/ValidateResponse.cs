namespace TopstepX.Models.Auth
{
    public class ValidateResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public string newToken { get; set; } = string.Empty;
    }
}