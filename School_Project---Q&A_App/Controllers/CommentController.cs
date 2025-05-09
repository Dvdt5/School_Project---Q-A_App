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
    [Authorize(Roles = "Member , Admin")]
    public class CommentController : Controller
    {
        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;
        private readonly IMapper _mapper;

        public CommentController(CommentRepository commentRepository, IMapper mapper, PostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<CommentDto>> List()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDtos = _mapper.Map<List<CommentDto>>(comments);
            return commentDtos;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<CommentDto> GetById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            var commentDto = _mapper.Map<CommentDto>(comment);
            return commentDto;
        }

        [HttpPost]
        public async Task<ResultDto> Add(CommentDto commentDto)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Comment Created Successfuly!"
            };
            commentDto.Created = DateTime.Now;
            commentDto.Updated = DateTime.Now;
            commentDto.Post.AnswerCount++;

            var comment = _mapper.Map<Comment>(commentDto);
            await _commentRepository.AddAsync(comment);
            return response;
        }

        [HttpPut]
        public async Task<ResultDto> Update(CommentDto commentDto)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "comment Updated Successfuly!"
            };
            commentDto.Updated = DateTime.Now;
            var comment = await _commentRepository.GetByIdAsync(commentDto.Id);
            comment.Content = commentDto.Content;


            await _commentRepository.UpdateAsync(comment);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ResultDto> Delete(int id)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Comment Deleted Successfuly!"
            };
            var comment = await _commentRepository.GetByIdAsync(id);
            comment.Post.AnswerCount--;
            await _commentRepository.DeleteAsync(id);
            return response;
        }


        [HttpGet("CommentByPost/{postId}")]
        [AllowAnonymous]
        public async Task<List<CommentDto>> GetCommentByPostId(int id)
        {
            var comments = await _commentRepository.GetAllAsync();
            var returnList = new List<Comment>();
            foreach (var comment in comments)
            {
                if (comment.PostId == id)
                {
                    returnList.Add(comment);
                }
                
            }
            var commentDtos = _mapper.Map<List<CommentDto>>(returnList);
            return commentDtos;
        }

    }
}
