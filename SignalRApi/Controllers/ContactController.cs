using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.ContactDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ContactGet()
        {
            var list = _mapper.Map<List<ResultContactDto>>(_contactService.TGetListAll());
            return Ok(list);
           
        }
        [HttpPost]
        public IActionResult ContactPost(CreateContactDto createContactDto)
        {
            Contact contact = new Contact()
            {
                ContactID = createContactDto.ContactID,
                FooterDescription = createContactDto.FooterDescription,
                Location = createContactDto.Location,
                Mail = createContactDto.Mail,
                Phone = createContactDto.Phone,

            };
             _contactService.TAdd(contact);
            return Ok("Contact Eklendi");

        }
        [HttpPut]
        public IActionResult ContactPut(UpdateContactDto updateContactDto)
        {
            Contact contact = new Contact()
            {
                ContactID = updateContactDto.ContactID,
                FooterDescription = updateContactDto.FooterDescription,
                Location = updateContactDto.Location,
                Mail = updateContactDto.Mail,
                Phone = updateContactDto.Phone,
            };
            _contactService.TUpdate(contact);
            return Ok("Contact Güncellendi");
        }
        [HttpDelete]
        public IActionResult ContactDelete(int id)
        {
            var getid = _contactService.TGetById(id);
            _contactService.TDelete(getid);
            return Ok("Contact Silindi");
        }
        [HttpGet("GetOneContact")]
        public IActionResult GetOneContact (int id)
        {
            var getid = _contactService.TGetById(id);
            return Ok(getid);
        }
    }
}
