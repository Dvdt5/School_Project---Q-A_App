using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;

namespace School_Project___Q_A_App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : Controller
    {
        private readonly PostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(PostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<PostDto>> List()
        {
            var posts = await _postRepository.GetAllAsync();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            return postDtos;
        }

        [HttpGet("{id}")]
        public async Task<PostDto> GetById(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        [HttpPost]
        public async Task<ResultDto> Add(PostDto postDto)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Post Created Successfuly!"
            };
            postDto.Created = DateTime.Now;
            postDto.Updated = DateTime.Now;
            postDto.Is_Active = true;
            postDto.UserId = 5;
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
