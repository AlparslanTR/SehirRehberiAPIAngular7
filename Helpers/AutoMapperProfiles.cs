using AutoMapper;
using SehirRehberiAPI.Dtos;
using SehirRehberiAPI.Models;
using System.Linq;

namespace SehirRehberiAPI.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>().ForMember(x => x.PhotoUrl, opt =>
            {
                opt.MapFrom(src => src.photos.FirstOrDefault(x => x.IsMain).Url);
            });

            //CreateMap<City, CityForDetailsDto>();
            CreateMap<PhotoForCreationDto,Photo >();
            CreateMap<PhotoForReturnDto, Photo>();
        }
    }
}
