using AutoMapper;
using TaskManagement.DTOs;
using TaskManagement.DTOs.Auth;
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

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();

            CreateMap<RoleVM, RoleDTO>();
            CreateMap<RoleDTO, RoleVM>();

            CreateMap<LoginVM, LoginDTO>();
            CreateMap<LoginDTO, LoginVM>();

            CreateMap<RegisterVM, RegisterDTO>();
            CreateMap<RegisterDTO, RegisterVM>();

            CreateMap<RegisterDTO, User>();
            CreateMap<User, RegisterDTO>();

            CreateMap<ChangePasswordVM, ChangePasswordDTO>();
            CreateMap<ChangePasswordDTO, ChangePasswordVM>();
        }
    }
}
