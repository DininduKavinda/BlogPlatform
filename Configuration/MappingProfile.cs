using AutoMapper;
using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Models.ViewModels;

namespace TaskManagement.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();

            CreateMap<CategoryVM, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryVM>();
        }
    }
}
