using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.TestimonialDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimontalService _testimontalService;
        private readonly IMapper _mapper;

        public TestimonialController(ITestimontalService testimontalService, IMapper mapper)
        {
            _testimontalService = testimontalService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult TestimonialGet()
        {
            var getlist = _mapper.Map<List<ResultTestimonialDto>>(_testimontalService.TGetListAll());
            return Ok(getlist);
        }
        [HttpPost]
        public IActionResult TestimonialPost(CreateTestimonialDto createTestimonialDto) 
        {
            Testimonial testimonial = new Testimonial()
            {
                TestimonialID = createTestimonialDto.TestimonialID,
                Title = createTestimonialDto.Title,
                Comment = createTestimonialDto.Comment,
                ImageUrl = createTestimonialDto.ImageUrl,
                Name = createTestimonialDto.Name,
                Status = createTestimonialDto.Status,
            };
            _testimontalService.TAdd(testimonial);
            return Ok("Testimonial Güncelleme Yapıldı");
        }
        [HttpPut]
        public IActionResult TestimonialPut(UpdateTestimonialDto updateTestimonialDto)
        {
            var testimonial = new Testimonial()
            {
                Comment = updateTestimonialDto.Comment,
                ImageUrl = updateTestimonialDto.ImageUrl,
                Name = updateTestimonialDto.Name,
                Status = updateTestimonialDto.Status,
                TestimonialID = updateTestimonialDto.TestimonialID,
                Title = updateTestimonialDto.Title,
            };

            _testimontalService.TUpdate(testimonial);
            return Ok("Güncelleme Yapıldı");

        }
        [HttpDelete]
        public IActionResult TestimonialDelete(int id)
        {
            var getid = _testimontalService.TGetById(id);
            _testimontalService.TDelete(getid);
            return Ok("testimonial silindi");
        }
        [HttpGet("TestimonialOneId")]
        public IActionResult TestimonialServiceGetOneId(int id)
        {
            var getid = _testimontalService.TGetById(id);
            return Ok(getid);
        }
    }
}
