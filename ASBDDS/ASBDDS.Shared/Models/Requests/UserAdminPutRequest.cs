using System;
using ASBDDS.Shared.Models.APIBaseModels.User;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.Requests
{
    public class UserAdminPutRequest : UserAPIBaseModel
    {
        public Guid Id { get; set; }
        public bool Disabled { get; set; }
        public UserAdminPutRequest() {}
    }
}