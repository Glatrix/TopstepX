namespace TopstepX.Models.Auth
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string apiKey { get; set; }

        public LoginRequest(string userName, string apiKey)
        {
            this.userName = userName;
            this.apiKey = apiKey;
        }
    }
}