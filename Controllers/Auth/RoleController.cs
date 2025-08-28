using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers.Auth
{
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService, IMapper mapper)
        {
            _logger = logger;
            _roleService = roleService;
            _mapper = mapper;
        }

        
    }
}