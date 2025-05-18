using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;

namespace School_Project___Q_A_App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "NewUser , Member , Admin")]
    public class PostController : Controller
    {
        private readonly PostRepository _postRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public PostController(PostRepository postRepository, IMapper mapper, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<PostDto>> List()
        {
            var posts = await _postRepository.GetAllAsync();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            return postDtos;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<PostDto> GetById(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post);
            var user = _userManager.Users.Where(s => s.Id == postDto.UserId).SingleOrDefault();
            postDto.User = user;
            return postDto;
        }

        [HttpGet("GetByCategoryId/{categoryId}")]
        [AllowAnonymous]
        public async Task<List<PostDto>> GetByCategoryId(int categoryId)
        {
            var posts = await _postRepository.GetAllAsync();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            var filteredPosts = postDtos.Where(s => s.CategoryId == categoryId).ToList();
            return filteredPosts;
        }

        [HttpPost]
        [Authorize(Roles = "NewUser , Member , Admin")]
        public async Task<ResultDto> Add(PostDto postDto)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Post Created Successfuly!"
            };
            postDto.Created = DateTime.Now;
            postDto.Updated = DateTime.Now;
            postDto.AnswerCount = 0;

            var post = _mapper.Map<Post>(postDto);
            await _postRepository.AddAsync(post);
            return response;
        }

        [HttpPut]
        public async Task<ResultDto> Update(PostDto postDto)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Post Updated Successfuly!"
            };
            postDto.Updated = DateTime.Now;
            var post = await _postRepository.GetByIdAsync(postDto.Id);
            post.Title = postDto.Title;
            post.Content = postDto.Content;
            post.Is_Active = postDto.Is_Active;
            post.CategoryId = postDto.CategoryId;

            
            await _postRepository.UpdateAsync(post);
            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "NewUser , Member , Admin")]
        public async Task<ResultDto> Delete(int id)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Post Deleted Successfuly!"
            };
            await _postRepository.DeleteAsync(id);
            return response;
        }

    }
}
