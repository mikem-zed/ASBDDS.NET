using System;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.APIBaseModels.User
{
    public class UserAPIBaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserAPIBaseModel() { }
        public UserAPIBaseModel(ApplicationUser user)
        {
            Name = user.Name;
            LastName = user.LastName;
            Email = user.Email;
        }
    }
}