using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Användarnamn är obligatoriskt")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Användarnamnet måste innehålla minst 2 tecken")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; } 

        [Required(ErrorMessage ="E-post är obligatoriskt")]
        [EmailAddress(ErrorMessage ="Ogiltig e-postadress")]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Lösenord är obligatoriskt")]
        [StringLength(50, MinimumLength = 5, ErrorMessage ="Lösenord måste innehålla minst 5 tecken")]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Roll")]
        public int RoleId { get; set; }

    }
}
