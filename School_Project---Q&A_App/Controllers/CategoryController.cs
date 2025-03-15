using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;

namespace School_Project___Q_A_App.Controllers
{
    [Route("Category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(CategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CategoryDto>> List()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }

        [HttpGet("{id}")]
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
        public async Task<Category> Update(CategoryDto categoryDto)
        {
            categoryDto.Updated = DateTime.Now;
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateAsync(category);
            return category;
        }

        [HttpDelete]
        public async Task<List<Category>> Delete(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return await _categoryRepository.GetAllAsync();
        }
    }
}
