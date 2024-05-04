using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductGet()
        {
            var list = _mapper.Map<List<ResultProductDto>>(_productService.TGetListAll());
            return Ok(list);
        }
        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var context = new SignalRContext();
            var values = context.Products.Include(x => x.Category).Select(y => new ResultCategorywithProduct
            {
                Description = y.Description,
                ImageUrl = y.ImageUrl,
                Price = y.Price,
                ProductID = y.ProductID,
                ProductName = y.ProductName,
                Status = y.Status,
                CategoryName = y.Category.CategoryName,


            });
            
            return Ok(values);
        }



        [HttpPost]
        public IActionResult ProductPost(CreateProductDto createProductDto)
        {
            Product product = new Product()
            {
                Description = createProductDto.Description,
                ImageUrl = createProductDto.ImageUrl,
                Price = createProductDto.Price,
                ProductID = createProductDto.ProductID,
                ProductName = createProductDto.ProductName,
                Status = createProductDto.Status,
                CategoryId = createProductDto.CategoryId,
                
            };
            _productService.TAdd(product);
            return Ok("Product Oluşturuldu");
        }
        [HttpPut]
        public IActionResult ProductUpdate(UpdateProductDto updateProductDto)
        {
            Product product = new Product()
            {
                Description = updateProductDto.Description,
                ImageUrl = updateProductDto.ImageUrl,
                Price = updateProductDto.Price,
                ProductID = updateProductDto.ProductID,
                ProductName = updateProductDto.ProductName,
                Status = updateProductDto.Status,
            };
            _productService.TUpdate(product);
            return Ok("Product Güncellendi");
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id) 
        {
            var getid = _productService.TGetById(id);
            return Ok(getid);
        }
        [HttpGet("GetOneProduct")]
        public IActionResult GetOneProduct(int id) 
        {
            var list = _productService.TGetById(id);
            return Ok(list);
        
        }
    }
}
