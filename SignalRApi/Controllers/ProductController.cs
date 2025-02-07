﻿using AutoMapper;
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

        [HttpGet("ProductCount")]
        public IActionResult ProductCount() 
        {
        return Ok(_productService.TProductCount());
        
        }

		[HttpGet("ProductCountByHamburger")]
		public IActionResult ProductCountByHamburger()
		{
			return Ok(_productService.TProductCountByCategoryNameHamburger());

		}

		[HttpGet("ProductCountByDrink")]
		public IActionResult ProduProductCountByDrinkctCount()
		{
			return Ok(_productService.TProductCountByCategoryNameDrink());

		}
		[HttpGet("ProductPriceAvg")]
		public IActionResult ProductPriceAvg()
		{
			return Ok(_productService.TProductPriceAvg());

		}
		[HttpGet("ProductNameWithMaxPrice")]
		public IActionResult ProductNameWithMaxPrice()
		{
			return Ok(_productService.TProductNameByMaxPrice());
		}
		[HttpGet("ProductNameWithMinPrice")]
		public IActionResult ProductNameWithMinPrice()
		{
			return Ok(_productService.TProductNameByMinPrice());
		}
		[HttpGet("ProductAvgPriceByHamburger")]
		public IActionResult ProductAvgPriceByHamburger()
		{
			return Ok(_productService.TProductAvgPriceByHamburger());
		}

		[HttpPost]
        public IActionResult ProductPost(CreateProductDto createProductDto)
        {
            Product product = new Product()
            {
				ProductID = createProductDto.ProductID,
				ProductName = createProductDto.ProductName,
				Description = createProductDto.Description,
                ImageUrl = createProductDto.ImageUrl,
                Price = createProductDto.Price,
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
                CategoryId = updateProductDto.CategoryID,
            };
            _productService.TUpdate(product);
            return Ok("Product Güncellendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id) 
        {
            var getid = _productService.TGetById(id);
            return Ok(getid);
        }
        [HttpGet("{id}")]
        public IActionResult GetOneProduct(int id) 
        {
            var list = _productService.TGetById(id);
            return Ok(list);
        
        }
    }
}
