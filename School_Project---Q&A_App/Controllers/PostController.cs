using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;

namespace School_Project___Q_A_App.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
        public async Task<Post> Add(PostDto postDto)
        {
            postDto.Created = DateTime.Now;
            postDto.Updated = DateTime.Now;
            var post = _mapper.Map<Post>(postDto);
            await _postRepository.AddAsync(post);
            return post;
        }

        [HttpPut]
        public async Task<Post> Update(PostDto postDto)
        {
            postDto.Updated = DateTime.Now;
            var post = _mapper.Map<Post>(postDto);
            await _postRepository.UpdateAsync(post);
            return post;
        }

        [HttpDelete]
        public async Task<List<Post>> Delete(int id)
        {
            await _postRepository.DeleteAsync(id);
            var posts = await _postRepository.GetAllAsync();
            return posts;
        }

    }
}
