using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SehirRehberiAPI.Data;
using SehirRehberiAPI.Dtos;
using SehirRehberiAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace SehirRehberiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private IAuthRepository authRepository;
       private IConfiguration configuration;
        private IGenericRepository repository;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration,IGenericRepository repository)
        {
            this.authRepository = authRepository;
            this.configuration = configuration;
            this.repository = repository;
        }
        [HttpGet]
        [Route("getAllUser")]
        public ActionResult GetAllUser()
        {
            var get = repository.GetUsers();
            return Ok(get);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (await authRepository.UserExists(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName", "UserName already exists");
            }
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };
            var createdUser=await authRepository.Register(userCreate,userForRegisterDto.Password);           
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);
            if (user==null)
            {
                return Unauthorized();
            }
            var tokenHandler=new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Appsettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString=tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }
    }
}
