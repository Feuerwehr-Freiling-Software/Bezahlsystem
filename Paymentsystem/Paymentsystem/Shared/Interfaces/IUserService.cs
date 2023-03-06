namespace Paymentsystem.Shared.Interfaces
{
    public interface IUserService
    {
        //public List<ApplicationUser> GetAllUsers();
        //public ApplicationUser? GetById(string id);
        //public ApplicationUser? GetByUsername(string username);
        //public ApplicationUser? GetByEmail(string email);
        //public int AddUser(ApplicationUser user);
        //public int UpdateUser(ApplicationUser user);
        public int DeleteUser(string id);
    }
}
