using FribergCarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentals.Data
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Add(User user)
        {
            applicationDbContext.Users.Add(user);
            applicationDbContext.SaveChanges();
        }

        public void Delete(User user)
        {
            applicationDbContext.Users.Remove(user);
            applicationDbContext.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return applicationDbContext.Users.Include(b => b.Bookings).Include(r=>r.Role).OrderBy(u => u.UserName);
        }

        public User GetUserById(int userId)
        {
            return applicationDbContext.Users.Include(b=>b.Bookings).Include(r=>r.Role).FirstOrDefault(u => u.UserId == userId);
        }

        public void Update(User user)
        {
            applicationDbContext.Users.Update(user);
            applicationDbContext.SaveChanges();
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return applicationDbContext.Users.Include(r=>r.Role).FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool HasEmailOrPassword(string email, string password)
        {
            return applicationDbContext.Users.Any(u => u.Email == email && u.Password == password || u.Email == email);
        }
    }
}
