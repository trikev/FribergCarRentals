using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public interface IBooking
    {

        void Add(Booking booking);
        void Delete(Booking booking);
        void Update(Booking booking);
        IEnumerable<Booking> GetAllBookings();
        Booking GetBookingById(int bookingId);
        public IEnumerable<Booking> GetAllBookingsById(int userId);
        public bool HasBookings(int carId);
    }
}
