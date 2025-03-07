using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;

namespace School_Project___Q_A_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<User>> List()
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        [HttpPost]
        public async Task<User> Add(User user)
        {
            user.Created = DateTime.Now;
            user.Updated = DateTime.Now;
            await _userRepository.AddAsync(user);
            return user;
        }

        [HttpPut]
        public async Task<User> Update (User user)
        {
            user.Updated = DateTime.Now;
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
