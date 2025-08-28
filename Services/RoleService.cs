using AutoMapper;
using TaskManagemen.Services.Interfaces;
using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;

namespace TaskManagement.Services
{
    public class RoleService : Service<Role, RoleDTO>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper) : base(roleRepository, mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
    }
}