using System;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.APIBaseModels.User
{
    public class UserAPIResponseBaseModel : UserAPIBaseModel
    {
        /// <summary>
        /// User ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User creation date
        /// </summary>
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