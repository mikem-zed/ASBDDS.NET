using ASBDDS.Shared.Models.APIBaseModels.User;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.Requests
{
    public class UserAdminPostRequest : UserAPIBaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Disabled { get; set; }
    }
}