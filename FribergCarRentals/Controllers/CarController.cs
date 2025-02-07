using FribergCarRentals.Data;
using Microsoft.AspNetCore.Mvc;
using FribergCarRentals.ViewModels;

namespace FribergCarRentals.Controllers
{

    public class CarController : Controller
    {
        private readonly ICar carRepository;

        public CarController(ICar carRepository)
        {
            this.carRepository = carRepository;
        }

        //GET: CarController/ListAllCars
        public ActionResult ListAllCars()
        {
            return View(carRepository.GetAllCars());
        }

        
        //GET: Car/ListAvailableCars
        public ActionResult ListAvailableCars(CreateBookingViewModel createBookingVM)
        {
            
            var cars = carRepository.GetAvailableCars(createBookingVM.StartDate, createBookingVM.EndDate);
            if (cars.Any())
            {
                ViewBag.StartDate = createBookingVM.StartDate;
                ViewBag.EndDate= createBookingVM.EndDate;
                return View(cars);
            }
            else
            {
                ViewBag.AlertMessage = "Det finns inga bilar tillgängliga under vald period";
            }
            return View(cars);
        }

        //GET: Car/CarPicture/5
        public ActionResult CarPicture(int carId)
        {
            return View(carRepository.GetCarById(carId));
        }



    }
}
