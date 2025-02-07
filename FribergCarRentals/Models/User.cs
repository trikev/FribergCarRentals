using Microsoft.AspNetCore.Identity;
using FribergCarRentals.Areas;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Models;



public class User
{
    public int UserId { get; set; }

    [Required]
    [Display(Name = "Användarnamn")]
    public string? UserName { get; set; }

    [Required]
    [Display(Name = "E-post")]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Lösenord")]
    public string Password { get; set; }

    [Required]
    [Display(Name = "RollId")]
    public int RoleId { get; set; }
    
    [Display(Name = "Roll")]
    public Role? Role { get; set; }

    public ICollection<Booking>? Bookings { get; set; }


    
    
}
