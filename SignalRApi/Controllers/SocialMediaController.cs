using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.ProductDto;
using SignalR.DtoLayer.SocialMediaDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;
        private readonly IMapper _mapper;

        public SocialMediaController(ISocialMediaService socialMediaService, IMapper mapper)
        {
            _socialMediaService = socialMediaService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductGet()
        {
            var list = _mapper.Map<List<ResultProductDto>>(_socialMediaService.TGetListAll());
            return Ok(list);
        }
        [HttpPost]
        public IActionResult SocialMediaPost(CreateSocialMediaDto createSocialMediaDto)
        {
            SocialMedia socialMedia = new SocialMedia()
            {
                SocialMediaID = createSocialMediaDto.SocialMediaID,
                Icon = createSocialMediaDto.Icon,
                Title = createSocialMediaDto.Title,
                Url = createSocialMediaDto.Url,
            };
             _socialMediaService.TAdd(socialMedia);
            return Ok("Güncelleme Yapıldı");
        }
        [HttpPut]
        public IActionResult SocialMediaUpdate(UpdateSocialMediaDto updateSocialMediaDto)
        {
            SocialMedia socialMedia = new SocialMedia()
            {
                SocialMediaID = updateSocialMediaDto.SocialMediaID,
                Icon = updateSocialMediaDto.Icon,
                Title = updateSocialMediaDto.Title,
                Url = updateSocialMediaDto.Url,
            };

            _socialMediaService.TAdd(socialMedia);
            return Ok("Social Media Update");
        }
        [HttpDelete]
        public IActionResult SocialMediaDelete(int id )
        {
            var getid = _socialMediaService.TGetById(id);
             _socialMediaService.TDelete(getid);
            return Ok("Socail media delete");
        }
    }
}
