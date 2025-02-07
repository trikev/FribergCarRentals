using FribergCarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentals.Data
{
    public class BookingRepository : IBooking
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BookingRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Add(Booking booking)
        {
            applicationDbContext.Bookings.Add(booking);
            applicationDbContext.SaveChanges();
        }

        public void Delete(Booking booking)
        {
            applicationDbContext.Bookings.Remove(booking);
            applicationDbContext.SaveChanges();
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return applicationDbContext.Bookings.Include(u=>u.User).Include(c=>c.Car).OrderBy(b => b.StartDate);

        }

        public Booking GetBookingById(int bookingId)
        {
            return applicationDbContext.Bookings.Include(b => b.Car).Include(b => b.User).FirstOrDefault(b => b.BookingId == bookingId);
        }

        public void Update(Booking booking)
        {
            applicationDbContext.Bookings.Update(booking);
            applicationDbContext.SaveChanges();
        }
        public IEnumerable<Booking> GetAllBookingsById(int userId)
        {
            return applicationDbContext.Bookings.Include(b => b.Car).Include(b => b.User).Where(b => b.UserId == userId);
        }
        public bool HasBookings(int carId)
        {
            return applicationDbContext.Bookings.Any(b => b.CarId == carId);
        }
    }
}
