using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.EntityFramework;
using SignalR.DtoLayer.CategoryDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CategoryGet()
        {
            var getlist = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
            return Ok(getlist);
        }
        [HttpGet("CategoryCount")]
        public IActionResult CategoryCount()
        {
            return Ok(_categoryService.TCategoryCount());
        }
		[HttpGet("ActiveCategory")]
		public IActionResult ActiveCategory()
		{
			return Ok(_categoryService.TActiveCategory());
		}
		[HttpGet("PassiveCategory")]
		public IActionResult PassiveCategory()
		{
			return Ok(_categoryService.TPassiveCategory());
		}
		[HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            Category category = new Category()
            {
                CategoryID = createCategoryDto.CategoryID,
                CategoryName = createCategoryDto.CategoryName,
                Status = createCategoryDto.Status,
            };

            _categoryService.TAdd(category);
            return Ok("GüncellemeYapıldı");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var getid = _categoryService.TGetById(id);

            _categoryService.TDelete(getid);
            return Ok("Category Silinmiştir");
        }

        [HttpPut]
        public IActionResult UpgradeCategory(UpdateCategoryDto updateCategoryDto)
        {
            Category category = new Category()
            {
                CategoryID = updateCategoryDto.CategoryID,
                CategoryName = updateCategoryDto.CategoryName,

            };

            _categoryService.TUpdate(category);
            return Ok("Update Yapıldı");

        }
        [HttpGet("{id}")]
         public IActionResult GetOneCategory(int id)
        {
            var getid = _categoryService.TGetById(id);
            return Ok(getid);
        }




    }
}
