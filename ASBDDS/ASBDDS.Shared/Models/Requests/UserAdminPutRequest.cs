using System;
using ASBDDS.Shared.Models.APIBaseModels.User;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.Requests
{
    public class UserAdminPutRequest : UserAPIBaseModel
    {
        /// <summary>
        /// User ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Is user disabled
        /// </summary>
        public bool Disabled { get; set; }
        public UserAdminPutRequest() {}
    }
}