using FribergCarRentals.Data;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICar carRepo;

        public CarController(ICar carRepo)
        {
            this.carRepo = carRepo;
        }
        [HttpGet("carId")]
        public Car GetCarById(int carId)
        { 
            var car = carRepo.GetCarById(carId);
            return car;
        }
    }
}
