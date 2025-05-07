using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;

namespace School_Project___Q_A_App.Controllers
{
    [Route("Category")]
    [ApiController]
    [Authorize(Roles = "Member , Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly PostRepository _postRepository;
        private readonly IMapper _mapper;

        public CategoryController(CategoryRepository categoryRepository, IMapper mapper, PostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<CategoryDto>> List()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<CategoryDto> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        [HttpPost]
        public async Task<Category> Add(CategoryDto categoryDto)
        {
            categoryDto.Created = DateTime.Now;
            categoryDto.Updated = DateTime.Now;
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
            return category;
        }

        [HttpPut]
        public async Task<ResultDto> Update(CategoryDto categoryDto)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Category Updated Successfuly!"
            };
            
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            category.Name = categoryDto.Name;
            category.Is_Active = categoryDto.Is_Active;
            category.Updated = DateTime.Now;

            await _categoryRepository.UpdateAsync(category);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ResultDto> Delete(int id)
        {
            ResultDto response = new ResultDto
            {
                Success = true,
                Message = "Category Deleted Successfuly!"
            };
            var posts = await _postRepository.GetAllAsync();
            foreach (var post in posts)
            {
                if (post.CategoryId == id)
                {
                    response.Success = false;
                    response.Message = "Category has items, cannot be deleted!";
                    return response;
                }
            }
            await _categoryRepository.DeleteAsync(id);
            return response;
        }
    }
}
