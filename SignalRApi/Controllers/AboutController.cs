﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.EntityFramework;
using SignalR.DtoLayer.AboutDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            var values = _aboutService.TGetListAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            About about = new About()
            {
                AboutID = createAboutDto.AboutID,
                Title = createAboutDto.Title,
                Description = createAboutDto.Description,
                ImageUrl = createAboutDto.ImageUrl,
            };

            _aboutService.TAdd(about);
            return Ok("Hakkımda kısmı eklendi");
        }
        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {
            var value = _aboutService.TGetById(id);
            _aboutService.TDelete(value);
            return Ok("Hakkımda Silindi");
        }

        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto )
        {
            About about = new About()
            {
                AboutID = updateAboutDto.AboutID,
                Title = updateAboutDto.Title,
                Description = updateAboutDto.Description,
                ImageUrl = updateAboutDto.ImageUrl,
            };
            _aboutService.TUpdate(about);
            return Ok("Hakkımda Güncellendi");
        }
        [HttpGet("GetAbout")]
        public IActionResult GetAbout(int id)
        {
           var value =  _aboutService.TGetById(id);
            return Ok(value);
        }


    }
}
