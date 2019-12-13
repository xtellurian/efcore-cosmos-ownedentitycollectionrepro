using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace src.Model
{
    public class Site
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Building> Buildings { get; set; } = new Collection<Building>();
    }
}