using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public interface IRole
    {
        void Update(Role role);
        public Role GetRoleById(int roleId);
    }
}
