using TaskManagement.Services.Interfaces;
using TaskManagement.DTOs;
using AutoMapper;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class CategoryService : Service<Category, CategoryDTO>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _categoryMapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper categoryMapper) : base(categoryRepository, categoryMapper)
        {
            _categoryRepository = categoryRepository;
            _categoryMapper = categoryMapper;
        }

        public async Task<bool> CategoryNameAnyAsync(string categoryName)
        {
            return await _categoryRepository.AnyAsync(c => c.CategoryName == categoryName);
        }

        public override async Task<CategoryDTO> AddAsync(CategoryDTO dto)
        {
            var exists = await CategoryNameAnyAsync(dto.CategoryName);
            if (exists)
            {
                throw new InvalidCastException("Category with the same name already exists.");
            }
            return await base.AddAsync(dto);
        }

        public override async Task<CategoryDTO> UpdateAsync(int id, CategoryDTO dto)
        {
            var exists = await CategoryNameAnyAsync(dto.CategoryName);
            if (exists)
            {
                throw new InvalidCastException("Category with the same name already exists.");
            }
            return await base.UpdateAsync(id, dto);
        }
    }
}