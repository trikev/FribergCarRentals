
using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.ViewModels
{
    public class UserBookingViewModel
    {
        public int BookingId { get; set; }

        [Display (Name ="Bil")]
        public string Model { get; set; }

        [Display (Name ="Upphämtning")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Återlämning")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM}")]
        public DateTime EndDate { get; set; }

        
    }

}
