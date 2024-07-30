using Chinese_Sale.DAL;
using Chinese_Sale.Models;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Chinese_Sale.BL
{
    public class UserService : IUserService
    {
     private readonly IUserDal _userDal;
        private readonly ILogger<User> _logger;
        public UserService(IUserDal userDal, ILogger<User> logger)
        {
            this._userDal = userDal;
            this._logger = logger;
        }
        public async Task<int> AddUser(User u)
        {
            return await _userDal.AddUserAsync(u);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userDal.GetAllUsersAsync();
        }

        public async Task<User> GetUserByUserNameAndPassword(string userName, string password)
        {
            return await _userDal.GetUserByUserNameAndPasswordAsync(userName, password);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userDal.GetUserByIdAsync(id);
        }

        public async Task<User> UpdateUserDetails(User u)
        {
            return await _userDal.UpdateUserDetailsAsync(u);
        }

        public string GenerateToken(User user)
        {
            return _userDal.GenerateToken(user);
        }
    }
}
