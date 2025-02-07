using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        [Display(Name = "Roll")]
        public string RoleName { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
