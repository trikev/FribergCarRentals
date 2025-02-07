using FribergCarRentals.Models;
using Microsoft.EntityFrameworkCore;


namespace FribergCarRentals.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    }
}
