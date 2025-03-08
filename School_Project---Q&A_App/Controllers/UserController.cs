using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;

namespace School_Project___Q_A_App.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<UserDto>> List()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        [HttpPost]
        public async Task<User> Add(UserDto userDto)
        {
            userDto.Created = DateTime.Now;
            userDto.Updated = DateTime.Now;
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddAsync(user);
            return user;
        }

        [HttpPut]
        public async Task<User> Update (UserDto userDto)
        {
            userDto.Updated = DateTime.Now;
            var user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateAsync(user);
            return user;
        }

        [HttpDelete]
        public async Task<List<User>> Delete (int id)
        {
            await _userRepository.DeleteAsync(id);
            return await _userRepository.GetAllAsync();

        }
    }
}
