using FribergCarRentals.Data;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentals.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUser userRepository;
        private readonly IBooking bookingRepository;
        private readonly ICar carRepository;



        public BookingController(IUser userRepository, IBooking bookingRepository, ICar carRepository)
        {
            this.userRepository = userRepository;
            this.bookingRepository = bookingRepository;
            this.carRepository = carRepository;
        }


        //GET: Booking/ConfirmBooking/5
        public ActionResult ConfirmBooking(CreateBookingViewModel createBookingVM)
        {
            
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                var returnUrl = Url.Action("ListAvailableCars", "Car", new { createBookingVM.CarId, createBookingVM.StartDate, createBookingVM.EndDate });
                return RedirectToAction("Login", "Account", new { ReturnUrl = returnUrl });
            }

            CarBookingViewModel carBookingViewModel = new CarBookingViewModel
            {
                BookingVM = createBookingVM,
                Car = carRepository.GetCarById(createBookingVM.CarId)
            };

            return View(carBookingViewModel);
        }
        
        
        //GET: Booking/CreateBooking/5
        public ActionResult CreateBooking(int carId)
        {
            return View(carRepository.GetCarById(carId));
        }

        //POST: Booking/CreateBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBooking(CreateBookingViewModel createBookingVM) 
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (userId == null)
                {
                    var returnUrl = Url.Action("ListAvailableCars", "Car", new { createBookingVM.CarId, createBookingVM.StartDate, createBookingVM.EndDate });
                    return RedirectToAction("Login", "Account", new { ReturnUrl = returnUrl });
                }

                if (ModelState.IsValid)
                {
                    Booking booking = new Booking
                    {
                        CarId = createBookingVM.CarId,
                        StartDate = createBookingVM.StartDate,
                        EndDate = createBookingVM.EndDate,
                        UserId = int.Parse(userId),
                    };
                    bookingRepository.Add(booking);

                    return RedirectToAction("BookingConfirmation", new { bookingId = booking.BookingId });
                }
                return RedirectToAction("AvailableCarCheck", "Car");
            }
            catch
            {
                return NotFound();
            }
        }

      
        //GET: Booking/BookingConfirmation/5
        public ActionResult BookingConfirmation(int bookingId)
        {
            var booking = bookingRepository.GetBookingById(bookingId);
            if (booking == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(booking);
        }


        //GET: Booking/MyBookings
        public ActionResult MyBookings()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(bookingRepository.GetAllBookingsById(int.Parse(userId)).Where(b => b.EndDate > DateTime.Now));           
        }

        //GET: Booking/OldBookings
        public ActionResult OldBookings()
        {
            var userId = HttpContext.Session.GetString("UserId");

            return View(bookingRepository.GetAllBookingsById(int.Parse(userId)).Where(b => b.EndDate < DateTime.Now));
        }


        //GET: Booking/RemoveBooking/5
        public ActionResult RemoveBooking(int bookingId)
        {
            var booking = bookingRepository.GetBookingById(bookingId);

            if (booking == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(booking);
        }

        //POST: Booking/RemoveBooking/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveBooking(Booking booking)
        {
            try
            {
                bookingRepository.Delete(booking);
                return RedirectToAction("MyBookings");
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Booking/EditBooking/5
        public ActionResult EditBooking(int bookingId)
        {
            var booking = bookingRepository.GetBookingById(bookingId);

            if (booking == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(booking);

        }  

        // POST: Booking/EditBooking/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBooking(UserBookingViewModel userBookingVM)
        {
            try
            {
                var booking = bookingRepository.GetBookingById(userBookingVM.BookingId);

                var bookings = bookingRepository.GetAllBookings()
                    .Where(c => c.StartDate < userBookingVM.EndDate && c.EndDate > userBookingVM.StartDate)
                    .ToList();

                if (ModelState.IsValid)
                {
                    if (bookings.Any())
                    {
                        ViewBag.AlertMessage = "Bilen är redan bokad under vald period. Försök att ändra igen.";
                        return View(booking);
                    }
                    booking.StartDate = userBookingVM.StartDate;
                    booking.EndDate = userBookingVM.EndDate;
                    
                    bookingRepository.Update(booking);
                }
                return RedirectToAction("MyBookings");
            }
            catch
            {

                return View();
            }
        }

        //GET: Booking/CarDetails/5
        public ActionResult BookingDetails(int bookingId)
        {
            return View(bookingRepository.GetBookingById(bookingId));
        }
    }
}
