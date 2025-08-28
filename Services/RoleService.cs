using AutoMapper;
using TaskManagement.Services.Interfaces;
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

        public async Task<bool> RoleNameAnyAsync(string roleName)
        {
            return await _roleRepository.AnyAsync(r => r.RoleName == roleName);
        }

        public override async Task<RoleDTO> AddAsync(RoleDTO dto)
        {
            var roleExists = await RoleNameAnyAsync(dto.RoleName);
            if (roleExists)
            {
                throw new InvalidCastException("Role with the same name already exists.");
            }
            return await base.AddAsync(dto);
        }

        public override async Task<RoleDTO> UpdateAsync(int id, RoleDTO dto)
        {
            var roleExists = await RoleNameAnyAsync(dto.RoleName);
            if (roleExists)
            {
                throw new InvalidCastException("Role with the same name already exists.");
            }
            return await base.UpdateAsync(id, dto);
        }
    }
}