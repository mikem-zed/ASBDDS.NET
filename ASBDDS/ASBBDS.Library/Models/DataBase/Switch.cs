namespace ASBBDS.Library.Models.DataBase
{
    public class Switch
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public int RouterId { get; set; }
        public virtual Router Router { get; set; }
    }
}
