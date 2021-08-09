using ASBDDS.Shared.Models.APIBaseModels.User;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.Requests
{
    public class UserAdminPostRequest : UserAPIBaseModel
    {
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Is user disabled
        /// </summary>
        public bool Disabled { get; set; }
    }
}