using System;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.APIBaseModels.User
{
    public class UserAPIBaseModel
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User e-mail
        /// </summary>
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