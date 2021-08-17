namespace ASBDDS.Shared.Models.Requests
{
    public class TokenRefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}