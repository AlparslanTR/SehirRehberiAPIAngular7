using System.Collections.Generic;

namespace SehirRehberiAPI.Models
{
    public class User
    {
        public User()
        {
            cities = new List<City>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List <City> cities { get; set; }
    }
}
