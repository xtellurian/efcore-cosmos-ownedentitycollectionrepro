namespace src.Model
{
    public class Room : IdentifiedThing
    {
        public Room()
        {
        }
        public Room(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}