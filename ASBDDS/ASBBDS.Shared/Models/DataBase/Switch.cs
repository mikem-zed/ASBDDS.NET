namespace ASBBDS.Library.Models.DataBase
{
    public class Switch : DbBaseModel
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public virtual Router Router { get; set; }
    }
}
