using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public interface IUser
    {

        void Add(User user);
        void Delete(User user);
        void Update(User user);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        public User GetUserByEmailAndPassword(string email, string password);
        public bool HasEmailOrPassword(string email, string password);
        

    }
}
