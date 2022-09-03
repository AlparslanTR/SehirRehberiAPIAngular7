using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using SehirRehberiAPI.Data;
using SehirRehberiAPI.Dtos;
using SehirRehberiAPI.Helpers;
using SehirRehberiAPI.Models;
using System.Linq;
using System.Security.Claims;

namespace SehirRehberiAPI.Controllers
{
    [Route("api/cities/{cityid}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        IGenericRepository repository;
        IMapper mapper;
        IOptions<CloudinarySettings> cloudinarySet;
        Cloudinary cloudinary;

        public PhotosController(IGenericRepository repository, IMapper mapper, IOptions<CloudinarySettings> cloudinarySet)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.cloudinarySet = cloudinarySet;

            Account account = new Account(cloudinarySet.Value.CloudName, cloudinarySet.Value.ApiKey, cloudinarySet.Value.ApiSecret);
            cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public ActionResult AddPhotoForCity(int Id, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            var city=repository.GetCityById(Id);
            if (city==null)
            {
                return BadRequest("Could not find the city");
            }

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId!=city.UserId)
            {
                return Unauthorized();
            }

            var file=photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();
            if (file.Length>0)
            {
                using (var stream=file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File=new FileDescription(file.Name,stream)
                    };
                    uploadResult=cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Url.ToString(); // Veri Tabanına Ekleme
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = mapper.Map<Photo>(photoForCreationDto);
            photo.city= city;

            if (!city.photos.Any(x=>x.IsMain))
            {
                photo.IsMain= true;
            }
            city.photos.Add(photo);

            if (repository.SaveAll(photo))
            {
                var photoToReturn = mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new {Id=photo.Id},photoToReturn);
            }
            return BadRequest("Could not add photo");
        }
        [HttpGet("id",Name ="GetPhoto")]
        public ActionResult GetPhoto(int id)
        {
            var photoFromDb=repository.GetPhotoById(id);
            var photo= mapper.Map<PhotoForReturnDto>(photoFromDb);
            return Ok(photo);
        }
    }
}
