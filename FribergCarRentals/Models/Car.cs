using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        [Display(Name ="Årsmodell")]
        public int ProductionYear { get; set; }

        
        [Display(Name = "Bil")]
        public string? Model { get; set; }

        [Display(Name = "Maxhastighet")]
        [DisplayFormat(DataFormatString = "{0} km/h")]
        public int MaxSpeed { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0} :-")]
        [Display(Name = "Hyra/dag")]
        public int CostPerDay { get; set; }
        
        
        [Display(Name = "Bild")]
        public string? Picture { get; set; }
        
        public List<Booking>? Bookings { get; set; }
    }
}
