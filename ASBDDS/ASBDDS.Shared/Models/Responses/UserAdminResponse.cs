using System;
using ASBDDS.Shared.Models.APIBaseModels.User;
using ASBDDS.Shared.Models.Database.DataDb;

namespace ASBDDS.Shared.Models.Responses
{
    public class UserAdminResponse : UserAPIResponseBaseModel
    {
        public Guid? EditorId { get; set; }
        public string EditorViewName { get; set; }
        public Guid? CreatorId { get; set; }
        public string CreatorViewName { get; set; }
        public DateTime? Updated { get; set; }
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