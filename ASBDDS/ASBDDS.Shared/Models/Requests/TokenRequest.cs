namespace ASBDDS.Shared.Models.Requests
{
    public class TokenRequest
    {
        /// <summary>
        /// Username for login
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
    }
}