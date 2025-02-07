using FribergCarRentals.Data;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentals.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly IBooking bookingRepository;
        private readonly ICar carRepository;

        public CarController(IBooking bookingRepository, ICar carRepository)
        {
            this.bookingRepository = bookingRepository;
            this.carRepository = carRepository;
        }

        
        //GET: Admin/CarController/ListAllCars
        public ActionResult ListAllCars()
        {
            return View(carRepository.GetAllCars());
        }
        
        
        //GET: Admin/CarController/CarPicture
        public ActionResult CarPicture(int carId)
        {
            return View(carRepository.GetCarById(carId));
        }

        //GET: Admin/CarController/AddNewCar
        public ActionResult AddNewCar()
        {
            return View();
        }

        // POST: BookingController/AddNewCar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewCar(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carRepository.Add(car);
                }
                return RedirectToAction("ListAllCars");
            }
            catch
            {
                return View();
            }
        }


        // GET: AdminController/RemoveCar/5
        public ActionResult RemoveCar(int carId)
        {
            if (bookingRepository.HasBookings(carId))
            {
                ViewBag.AlertMessage = "Du kan inte ta bort en bil med en bokning.";
            }
            return View(carRepository.GetCarById(carId));
        }

        // POST: AdminController/RemoveCar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCar(Car car)
        {
            try
            {
                carRepository.Delete(car);
                return RedirectToAction("ListAllCars");
            }
            catch
            {
                return NotFound();
            }
        }
        
        
        // GET: Admin/CarController/EditCar/5
        public ActionResult EditCar(int carId)
        {
            return View(carRepository.GetCarById(carId));
        }

        // POST: Admin/CarController/EditCar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carRepository.Update(car);
                }
                return RedirectToAction("ListAllCars");
            }
            catch
            {
                return View();
            }
        }

    }
}
