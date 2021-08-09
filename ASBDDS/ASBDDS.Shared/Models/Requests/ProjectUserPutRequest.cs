using ASBDDS.Shared.Models.Database.DataDb;


namespace ASBDDS.Shared.Models.Requests
{
    public class ProjectUserPutRequest
    {
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Default vlan
        /// </summary>
        public int DefaultVlan { get; set; }
        
        public ProjectUserPutRequest(Project project)
        {
            Name = project.Name;
            DefaultVlan = project.DefaultVlan;
        }
        public ProjectUserPutRequest() { }
    }

}
