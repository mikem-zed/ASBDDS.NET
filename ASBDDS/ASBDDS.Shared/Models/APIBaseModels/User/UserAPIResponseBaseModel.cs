using System;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.APIBaseModels.User
{
    public class UserAPIResponseBaseModel : UserAPIBaseModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public UserAPIResponseBaseModel() {}
        public UserAPIResponseBaseModel(ApplicationUser user): base(user)
        {
            Created = user.Created;
            UserName = user.UserName;
            Id = user.Id;
        }
    }
}