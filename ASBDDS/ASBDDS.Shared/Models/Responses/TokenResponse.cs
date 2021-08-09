using System;

namespace ASBDDS.Shared.Models.Responses
{
    public class TokenResponse
    {
        /// <summary>
        /// Bearer token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Name of the user who received the token
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Token expiration date
        /// </summary>
        public DateTime Expires { get; set; }
    }
}