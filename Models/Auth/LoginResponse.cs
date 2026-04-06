namespace TopstepX.Models.Auth
{
    public class LoginResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
        public string token { get; set; } = string.Empty;
    }
}