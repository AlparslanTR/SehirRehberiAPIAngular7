using System.Collections.Generic;

namespace SehirRehberiAPI.Models
{
    public class City
    {
        public City()
        {
            photos = new List<Photo>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<Photo> photos { get; set; }
        public User User { get; set; }
    }
}
