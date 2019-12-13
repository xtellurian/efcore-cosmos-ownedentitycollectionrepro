namespace src.Model
{
    public class Room
    {
        public Room()
        {
        }
        public Room(string name)
        {
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}