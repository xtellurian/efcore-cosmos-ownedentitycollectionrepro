namespace src.Model
{
    public class Device
    {
        public Device()
        {
        }
        public Device(string name)
        {
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}