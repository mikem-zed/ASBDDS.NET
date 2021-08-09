using System;
using ASBDDS.Shared.Models.APIBaseModels.User;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.Responses
{
    public class UserAdminResponse : UserAPIResponseBaseModel
    {
        /// <summary>
        /// The ID of the last user who edit this user
        /// </summary>
        public Guid? EditorId { get; set; }
        /// <summary>
        /// The human readable name of the last user who edit this user
        /// </summary>
        public string EditorViewName { get; set; }
        /// <summary>
        /// The ID of the user who create this user
        /// </summary>
        public Guid? CreatorId { get; set; }
        /// <summary>
        /// The human readable name of the user who create this user
        /// </summary>
        public string CreatorViewName { get; set; }
        /// <summary>
        /// Last update date
        /// </summary>
        public DateTime? Updated { get; set; }
        /// <summary>
        /// Is user disabled
        /// </summary>
        public bool Disabled { get; set; }
        public UserAdminResponse() {}
        public UserAdminResponse(ApplicationUser user) : base(user)
        {
            Updated = user.Updated;
            Disabled = user.Disabled;
            EditorId = user.EditorId;
            CreatorId = user.CreatorId;
        }
    }
}