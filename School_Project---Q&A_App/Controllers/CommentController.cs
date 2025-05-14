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
    public class CommentController : Controller
    {
        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CommentController(CommentRepository commentRepository, IMapper mapper, PostRepository postRepository, UserRepository userRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _userManager = userManager;
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
        [Authorize(Roles = "NewUser , Member , Admin")]
        public async Task<ResultDto> Add(CommentDto commentDto)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Comment Created Successfuly!"
            };
            var newComment = new Comment();
            newComment.Content = commentDto.Content;
            newComment.PostId = commentDto.PostId;
            newComment.UserId = commentDto.UserId;
            var post = await _postRepository.GetByIdAsync(commentDto.PostId);
            newComment.Post = post;
            newComment.Post.AnswerCount += 1;
            newComment.Created = DateTime.Now;
            newComment.Updated = DateTime.Now;
            

            
            await _commentRepository.AddAsync(newComment);
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
        [Authorize(Roles = "NewUser , Member , Admin")]
        public async Task<ResultDto> Delete(int id)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Comment Deleted Successfuly!"
            };
            var comment = await _commentRepository.GetByIdAsync(id);
            
            await _commentRepository.DeleteAsync(id);
            return response;
        }


        [HttpGet("CommentByPost/{postId}")]
        [AllowAnonymous]
        public async Task<List<CommentDto>> GetCommentByPostId(int postId)
        {
            var comments = await _commentRepository.GetAllAsync();
            var returnList = new List<CommentDto>();
            foreach (var comment in comments)
            {
                if (comment.PostId == postId)
                {
                    var dtoComment = _mapper.Map<CommentDto>(comment);
                    dtoComment.UserName = _userManager.Users.Where(s => s.Id == dtoComment.UserId).SingleOrDefault().UserName;
                    returnList.Add(dtoComment);
                }
                
            }
            var commentDtos = _mapper.Map<List<CommentDto>>(returnList);
            return commentDtos;
        }

    }
}
