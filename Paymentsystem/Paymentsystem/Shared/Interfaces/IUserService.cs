namespace Paymentsystem.Shared.Interfaces
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User? GetById(string id);
        public User? GetByUsername(string username);
        public User? GetByEmail(string email);
        public int AddUser(User user);
        public int UpdateUser(User user);
        public int DeleteUser(string id);
        public User? GetByRefreshToken(string refreshtoken);
    }
}
