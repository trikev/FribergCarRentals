using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public class RoleRepository : IRole
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        public Role GetRoleById(int roleId)
        {
            return applicationDbContext.Roles.FirstOrDefault(r => r.RoleId == roleId);
        }

        public void Update(Role role)
        {
            applicationDbContext.Roles.Update(role);
            applicationDbContext.SaveChanges();
        }
    }
}
