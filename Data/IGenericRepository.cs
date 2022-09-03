using SehirRehberiAPI.Models;
using System.Collections.Generic;

namespace SehirRehberiAPI.Data
{
    public interface IGenericRepository
    {
        void Add<T>(T entity); //Ekleme İşlemi
        void Delete<T>(T entity); //Silme İşlemi
        bool SaveAll<T>(T entity); //Güncelleme İşlemi
        List<City> GetCities(); //Bütün Şehirleri Listeleme İşlemi
        List<Photo> GetPhotoByCity(int id); //Şehire Göre Resim Getirme İşlemi
        City GetCityById(int id); //Id ye Göre Şehir Getirme İşlemi
        Photo GetPhotoById(int id); //Id ye Göre Resim Getirme İşlemi
        List<User> GetUsers();  //Bütün Kullanıcıları Listeleme İşlemi
    }
}
