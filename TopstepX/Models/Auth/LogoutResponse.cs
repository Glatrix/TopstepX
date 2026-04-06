namespace TopstepX.Models.Auth
{
    public class LogoutResponse
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string? errorMessage { get; set; }
    }
}