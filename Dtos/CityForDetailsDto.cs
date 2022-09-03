using SehirRehberiAPI.Models;
using System.Collections.Generic;

namespace SehirRehberiAPI.Dtos
{
    public class CityForDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Photo>Photos { get; set; }
    }
}
