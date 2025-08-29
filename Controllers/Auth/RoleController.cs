using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Models.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            var role = await _roleService.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<RoleVM>>(role);
            return View(viewModel);
        }

        public async Task<IActionResult> Show(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            var viewModel = _mapper.Map<RoleVM>(role);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = _mapper.Map<RoleDTO>(viewModel);
                    await _roleService.AddAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidCastException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the role.");
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<RoleVM>(role);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoleVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = _mapper.Map<RoleDTO>(viewModel);
                    await _roleService.UpdateAsync(id, role);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidCastException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the role.");
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _roleService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}