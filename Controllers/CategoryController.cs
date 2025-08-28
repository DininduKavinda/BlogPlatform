using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Models.ViewModels;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper)
        {
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var viewModel = _mapper.Map<List<CategoryVM>>(categories);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var categoryDTO = _mapper.Map<CategoryDTO>(viewModel);
                await _categoryService.AddAsync(categoryDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<CategoryVM>(categoryDTO);
            return View(viewModel);
        }

        public async Task<IActionResult> Show(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<CategoryVM>(category);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var exists = await _categoryService.GetByIdAsync(id);
                if (exists == null)
                {
                    return NotFound();
                }
                try
                {
                    var categoryDTO = _mapper.Map<CategoryDTO>(viewModel);
                    await _categoryService.UpdateAsync(id, categoryDTO);
                }
                catch (System.Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                await _categoryService.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
