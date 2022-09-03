using Microsoft.EntityFrameworkCore;
using SehirRehberiAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace SehirRehberiAPI.Data
{
    public class GenericRepository : IGenericRepository
    {
        DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity)
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        public List<City> GetCities()
        {
            var get = _context.Cities.Include(x=>x.photos).ToList();
            return get;
        }
        
        public City GetCityById(int id)
        {
            var get =_context.Cities.Include(x => x.photos).FirstOrDefault(x => x.Id ==id);
            return get;
        }

        public List<Photo> GetPhotoByCity(int id)
        {
            var get =_context.Photos.Where(x => x.CityId ==id).ToList();
            return get;
        }

        public Photo GetPhotoById(int id)
        {
            var get = _context.Photos.FirstOrDefault(x => x.Id == id);
            return get;
        }

        public bool SaveAll<T>(T entity)
        {
            return _context.SaveChanges()>0;
        }
        public List<User> GetUsers()
        {
            var get = _context.Users.ToList();
            return get;
        }
    }
}
