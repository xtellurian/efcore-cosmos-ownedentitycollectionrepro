namespace src.Model
{
    public class Device : IdentifiedThing
    {
        public Device()
        {
        }
        public Device(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}