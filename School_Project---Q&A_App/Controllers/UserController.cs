using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace School_Project___Q_A_App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public UserController(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
        public List<UserDto> List()
        {
            var users = _userManager.Users.ToList();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        [HttpGet("{id}")]
        public LoginDto GetById(string id)
        {
            var user = _userManager.Users.Where(s => s.Id == id).SingleOrDefault();
            var userDto = _mapper.Map<LoginDto>(user);
            return userDto;
        }

        [HttpPost]
        public async Task<ResultDto> Add(RegisterDto userDto)
        {
            var userAdd = _mapper.Map<AppUser>(userDto);
            userAdd.UserName = userDto.UserName;
            userAdd.Created = DateTime.Now;
            userAdd.Updated = DateTime.Now;
            userAdd.Is_Active = false;

            var identityResult = await _userManager.CreateAsync(userAdd, userAdd.Password);

            if (!identityResult.Succeeded)
            {
                var errorMessages = new List<string>();
                foreach (var item in identityResult.Errors)
                {
                    errorMessages.Add("<p>" + item.Description + "<p>");
                }
                var responseBad = new ResultDto
                {
                    Success = false,
                    Message = errorMessages[0],
                };
                return responseBad;
            }

            var user = await _userManager.FindByNameAsync(userDto.UserName);
            var roleExist = await _roleManager.RoleExistsAsync("Member");
            if (!roleExist)
            {
                var role = new AppRole { Name = "Member" };
                await _roleManager.CreateAsync(role);
            }


            await _userManager.AddToRoleAsync(user, "Member");
            var response = new ResultDto
            {
                Success = true,
                Message = "User Added Successfuly!",
            };
            return response;
        }

        [HttpPut]
        public async Task<ResultDto> Update(UserDto userDto)
        {
            var response = new ResultDto
            {
                Success = true,
                Message = "User Updated Successfuly!",
            };
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            if (userDto.Password != null)
            {
                user.Password = userDto.Password;
            }
            user.Is_Active = userDto.Is_Active;
            user.Updated = DateTime.Now;

            await _userManager.UpdateAsync(user);
            return response;
        }

        [HttpDelete]
        public async Task<ResultDto> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);

            var response = new ResultDto
            {
                Success = true,
                Message = "User Deleted Successfuly!",
            };
            return response;

        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<LoginResponseDto> SignIn(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user is null)
            {
                var response = new LoginResponseDto
                {
                    Success = false,
                    Message = "This User Doesnt Exit!",
                };
                return response;
            }
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordCorrect)
            {
                var response = new LoginResponseDto
                {
                    Success = false,
                    Message = "Username or Password is wrong!",
                };
                return response;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("JWTID", Guid.NewGuid().ToString()),

            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateJWT(authClaims);

            var responseDone = new LoginResponseDto
            {
                Success = true,
                Message = "Login Successfull!",
                Token = token,
            };
            return responseDone;
        }
        private string GenerateJWT(List<Claim> claims)
        {

            var accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["AccessTokenExpiration"]));


            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var tokenObject = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: accessTokenExpiration,
            claims: claims,
            signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
   

    }
}
