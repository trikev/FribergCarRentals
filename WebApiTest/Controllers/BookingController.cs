using FribergCarRentals.Data;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBooking bookingRepo;

        public BookingController(IBooking bookingRepo)
        {
            this.bookingRepo = bookingRepo;
        }

        [HttpGet("{userId}")]
        public IEnumerable<Booking> Get(int userId)
        {
            var bookings = bookingRepo.GetAllBookingsById(userId).Where(b => b.EndDate > DateTime.Now);
            
            return bookings;
        }

        [HttpGet("{bookingId}")]
        public Booking BookingDetails(int bookingId)
        {
            var booking = bookingRepo.GetBookingById(bookingId);

            return booking;
        }

        [HttpPut]
        public ActionResult Put(UserBookingViewModel userBookingVM)
        {
            try
            {
                var booking = bookingRepo.GetBookingById(userBookingVM.BookingId);

                var bookings = bookingRepo.GetAllBookings()
                    .Where(c => c.StartDate < userBookingVM.EndDate && c.EndDate > userBookingVM.StartDate)
                    .ToList();

                if (ModelState.IsValid)
                {
                    if (bookings.Any())
                    {
                        return Conflict("Bilen är redan bokad under vald period. Försök att ändra igen.");
                        
                    }
                    booking.StartDate = userBookingVM.StartDate;
                    booking.EndDate = userBookingVM.EndDate;

                    bookingRepo.Update(booking);
                    return Ok(booking);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {

                return StatusCode(500, $"Ett fel har inträffade: {ex.Message}");
            }
        }

        [HttpDelete("{bookingId}")]
        public ActionResult Delete(int bookingId)
        {
            try
            {
                var booking = bookingRepo.GetBookingById(bookingId);

                bookingRepo.Delete(booking);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
