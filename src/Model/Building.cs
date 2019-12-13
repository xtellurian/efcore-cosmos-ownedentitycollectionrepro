using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace src.Model
{
    public class Building
    {
        public Building()
        {
        }
        public Building(string name)
        {
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Room> Rooms { get; set; } = new Collection<Room>();
        public virtual ICollection<Device> Devices { get; set; } = new Collection<Device>();
    }
}