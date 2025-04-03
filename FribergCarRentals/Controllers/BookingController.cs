using FribergCarRentals.Data;
using FribergCarRentals.Models;
using FribergCarRentals.Services;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentals.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUser userRepository;
        private readonly IBooking bookingRepository;
        private readonly ICar carRepository;
        private readonly ApiService apiService;



        public BookingController(IUser userRepository, IBooking bookingRepository, ICar carRepository, ApiService apiService)
        {
            this.userRepository = userRepository;
            this.bookingRepository = bookingRepository;
            this.carRepository = carRepository;
            this.apiService = apiService;
        }


        //GET: Booking/ConfirmBooking
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
        [HttpGet]
        public async Task<ActionResult> MyBookings()
        {

            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var bookings = await apiService.Get<IEnumerable<Booking>>($"Booking/Get/{int.Parse(userId)}");
            return View(bookings);


        }

        //GET: Booking/OldBookings
        public ActionResult OldBookings()
        {
            var userId = HttpContext.Session.GetString("UserId");

            return View(bookingRepository.GetAllBookingsById(int.Parse(userId)).Where(b => b.EndDate < DateTime.Now));
        }


        //GET: Booking/RemoveBooking/5
        public async Task<ActionResult> RemoveBooking(int bookingId)
        {
            var booking = await apiService.Get<Booking>($"Booking/BookingDetails/{bookingId}");

            if (booking == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(booking);
        }

        //POST: Booking/RemoveBooking/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveBookingAgain(int bookingId)
        {
            try
            {
                await apiService.Delete($"Booking/Delete/{bookingId}");          
                return RedirectToAction("MyBookings");
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Booking/EditBooking/5
        public async Task<ActionResult> EditBooking(int bookingId)
        {
            var booking = await apiService.Get<Booking>($"Booking/BookingDetails/{bookingId}");

            if (booking == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(booking);

        }

        // POST: Booking/EditBooking/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBooking(UserBookingViewModel userBookingVM)
        {
            await apiService.Put<UserBookingViewModel>($"Booking/Put", userBookingVM);
            return RedirectToAction("MyBookings");
        }

        //GET: Booking/CarDetails/5
        [HttpGet]
        public async Task<ActionResult> BookingDetails(int bookingId)
        {
            var booking = await apiService.Get<Booking>($"Booking/BookingDetails/{bookingId}");
            return View(booking);

        }
    }
}
