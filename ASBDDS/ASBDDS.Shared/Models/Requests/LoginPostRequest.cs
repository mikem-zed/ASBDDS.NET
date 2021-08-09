namespace ASBDDS.Shared.Models.Requests
{
    public class LoginPostRequest
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