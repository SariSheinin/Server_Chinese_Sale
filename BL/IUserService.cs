using Chinese_Sale.Models;

namespace Chinese_Sale.BL
{
    public interface IUserService
    {
        Task<User> GetUserByUserNameAndPassword(string userName, string password);
        Task<User> GetUserById(int id);
        Task<int> AddUser(User u);
        Task<User> UpdateUserDetails(User u);
        Task<List<User>> GetAllUsers();
        string GenerateToken(User user);

    }
}
