using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public interface ICar
    {
        void Add(Car car);
        void Delete(Car car);
        void Update(Car car);
        IEnumerable<Car> GetAllCars();
        Car GetCarById(int carId);
        IEnumerable<Car> GetAvailableCars(DateTime startDate, DateTime endDate);

    }
}
