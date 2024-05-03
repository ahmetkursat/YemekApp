using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DtoLayer.BookingDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult BookingList()
        {
            var values = _bookingService.TGetListAll();
            return(Ok(values) );
        }

        [HttpPost]
        public IActionResult CreateBook(CreateBookingDto createBookingDto)
        {
            Booking booking = new Booking()
            {
                BookingId = createBookingDto.BookingId,
                Date = createBookingDto.Date,
                PersoCount = createBookingDto.PersoCount,
                Mail = createBookingDto.Mail,
                Name = createBookingDto.Name,
                Phone = createBookingDto.Phone,
            };
             _bookingService.TAdd(booking);
            return Ok("Rezervasyon Eklendi");
        }
        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            var getid = _bookingService.TGetById(id);
            _bookingService.TDelete(getid);

            return Ok("Hakkımda Kısmı Silindi");
        }
        [HttpPut]
        public IActionResult UpdateBook(UpdateBookingDto updateBookingDto)
        {
            var booking = new Booking()
            {
                BookingId = updateBookingDto.BookingId,
                Date = updateBookingDto.Date,
                Mail = updateBookingDto.Mail,
                Name = updateBookingDto.Name,
                Phone = updateBookingDto.Phone,
                PersoCount = updateBookingDto.PersoCount,
            };

                _bookingService.TUpdate(booking);
            return Ok("Rezervasyon Update");


        }
        [HttpGet("GetBooking")]
        public IActionResult GetBook(int id)
        {
            var getid = _bookingService.TGetById(id);
            return Ok("Rezarvasyon getirildi");
        }

    }
}
