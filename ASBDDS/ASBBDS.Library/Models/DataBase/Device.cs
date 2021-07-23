namespace ASBBDS.Library.Models.DataBase
{
    public class Device
    {
        public int Id { get; set; }
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string BaseModel { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public int SwitchId { get; set; }
        public virtual Switch Switch { get; set; }
    }
}
