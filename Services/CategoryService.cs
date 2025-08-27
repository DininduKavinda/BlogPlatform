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
    }
}