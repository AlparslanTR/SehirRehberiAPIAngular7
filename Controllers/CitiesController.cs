using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SehirRehberiAPI.Data;
using SehirRehberiAPI.Dtos;
using SehirRehberiAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace SehirRehberiAPI.Controllers
{
    [Route("api/Cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        IGenericRepository repository;
        IMapper _mapper;
         DataContext _context;
        public CitiesController(IGenericRepository repository, IMapper mapper,DataContext context)
        {
            this.repository = repository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        // Bütün Şehirlerinin Listesini Getir
        public ActionResult GetCities()
        {
            var get = repository.GetCities();
            var getToReturn = _mapper.Map<List<CityForListDto>>(get);
            return Ok(getToReturn);
        }
        [HttpGet]
        [Route("details")]
        public ActionResult GetCitiesById(int id)
        {
            var get = repository.GetCityById(id);
            //var getToReturn = _mapper.Map<List<CityForDetailsDto>>(get);
            return Ok(get);
        }

        [HttpPost]
        // Şehir Tablosuna Ekleme Yapma İşlemi
        [Route("add")]
        public ActionResult Add([FromBody]City p)
        {
            repository.Add(p);
            repository.SaveAll(p);
            return Ok(p);
        }
        [HttpGet]
        [Route("photos")]
        public ActionResult GetPhotosByCity(int id)
        {
            var get = repository.GetPhotoByCity(id);
            return Ok(get);
        }
    }
}
