using FribergCarRentals.Models;

namespace FribergCarRentals.ViewModels
{
    public class CarBookingViewModel
    {
        public Car Car { get; set; }
        public CreateBookingViewModel BookingVM { get; set; }
    }
}
