using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.ContactDto;
using SignalR.DtoLayer.DiscountDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DiscountGet()
        {
            var list = _mapper.Map<List<ResultDiscountDto>>(_discountService.TGetListAll());

            return Ok(list);

        }
        [HttpPost]
        public IActionResult DiscountPost(CreateDiscountDto createDiscountDto)
        {
            Discount discount = new Discount()
            {
                Amount = createDiscountDto.Amount,
                Description = createDiscountDto.Description,
                DiscountID = createDiscountDto.DiscountID,
                ImageUrl = createDiscountDto.ImageUrl,
                Title = createDiscountDto.Title
            };
            _discountService.TAdd(discount);
            return Ok(discount);

        }
        [HttpDelete]
        public IActionResult DiscountDelete(int id)
        {
            var getid = _discountService.TGetById(id);
            _discountService.TDelete(getid);
            return Ok("Silindi");
        }
        [HttpPut]
        public IActionResult DiscountUpdate(CreateDiscountDto createDiscountDto)
        {
            Discount discount = new Discount()
            {
                Amount = createDiscountDto.Amount,
                Description = createDiscountDto.Description,
                DiscountID = createDiscountDto.DiscountID,
                ImageUrl = createDiscountDto.ImageUrl,
                Title = createDiscountDto.Title
            };
            _discountService.TUpdate(discount);
            return Ok(discount);
        }
        [HttpGet("GetOneDiscount")]
        public IActionResult GetOneDiscount(int id)
        {
            var list = _discountService.TGetById(id);
            return Ok(list);  
        }    
        

    }
}
