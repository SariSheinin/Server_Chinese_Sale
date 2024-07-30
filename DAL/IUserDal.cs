using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;

namespace Chinese_Sale.DAL
{
    public interface IUserDal
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByUserNameAndPasswordAsync(string userName, string password);
        Task<User> GetUserByIdAsync(int id);
        Task<int> AddUserAsync(User u);
        Task<User> UpdateUserDetailsAsync(User u);
        //Task<User> GetUserAsync();
        string GenerateToken(User user);

    }
}
