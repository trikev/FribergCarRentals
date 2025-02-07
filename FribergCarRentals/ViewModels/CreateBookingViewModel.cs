using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.ViewModels
{
    public class CreateBookingViewModel
    {
        
        public int CarId { get; set; }

        [Required]
        [Display(Name = "Upphämtning")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Återlämning")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM}")]
        public DateTime EndDate { get; set; }

    }
}
