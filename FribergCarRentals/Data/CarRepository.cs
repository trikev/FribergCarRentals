using FribergCarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentals.Data
{
    public class CarRepository : ICar
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CarRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public void Add(Car car)
        {
            applicationDbContext.Cars.Add(car);
            applicationDbContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            applicationDbContext.Cars.Remove(car);
            applicationDbContext.SaveChanges();
        }

        public IEnumerable<Car> GetAllCars()
        {
            return applicationDbContext.Cars.OrderBy(c => c.Model);
        }

        public IEnumerable<Car> GetAvailableCars(DateTime startDate, DateTime endDate)
        {
            return applicationDbContext.Cars.Where(c => !c.Bookings.Any(booking => (startDate < booking.EndDate && endDate > booking.StartDate))).ToList();
        }

        public Car GetCarById(int carId)
        {
            return applicationDbContext.Cars.Include(b=>b.Bookings).FirstOrDefault(c => c.CarId == carId);
        }

        public void Update(Car car)
        {
            applicationDbContext.Cars.Update(car);
            applicationDbContext.SaveChanges();
        }

        
    }
}
