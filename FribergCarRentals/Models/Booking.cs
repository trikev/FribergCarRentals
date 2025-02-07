using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [Display(Name = "Upphämtning")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Återlämning")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM}")]
        public DateTime EndDate { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Car Car { get; set; }
    }

}
