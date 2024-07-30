using Chinese_Sale.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sale_server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chinese_Sale.DAL
{
    public class UserDal:IUserDal
    {
        private readonly SaleContext _saleContext;
        private readonly ILogger<User> _logger;
        private IConfiguration _config;

        public UserDal(SaleContext saleContext , ILogger<User> logger, IConfiguration config)
        {
            this._saleContext = saleContext;
            this._logger = logger;
            this._config = config;
        }
        public async Task<User> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            try
            {
                User user = await _saleContext.User.FirstOrDefaultAsync(u=> u.UserName == userName && u.Password == password);
                return user;
            }

            catch (Exception ex)
            {
                _logger.LogError("Logging from user, the exception: " + ex.Message, 1);
                throw new Exception("Logging from user, the exception: " + ex.Message);
            }
        }
    
        public async Task<User> GetUserByIdAsync(int id)
        {
        try
        {
            User user = await _saleContext.User.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        catch (Exception ex)
        {
            _logger.LogError("Logging from user, the exception: " + ex.Message, 1);
            throw new Exception("Logging from user, the exception: " + ex.Message);
        }
    }

        public async Task<int> AddUserAsync(User u)
        {
            try
            {
                await _saleContext.User.AddAsync(u);
                await _saleContext.SaveChangesAsync();
                return u.Id;
            }

            catch (Exception ex)
            {
                _logger.LogError("Logging from user, the exception: " + ex.Message, 1);
                throw new Exception("Logging from user, the exception: " + ex.Message);
            }
        }

        public async Task<User> UpdateUserDetailsAsync(User u)
        {
            try
            {
                User userToEdit = await _saleContext.User.FirstOrDefaultAsync(item => item.Id == u.Id);
                if (userToEdit != null)
                {
                    userToEdit.Id = u.Id;
                    userToEdit.FullName = u.FullName;
                    userToEdit.Password = u.Password;
                    userToEdit.Phone = u.Phone;
                    userToEdit.Email = u.Email;
                    userToEdit.IsAdmin = u.IsAdmin;

                    _saleContext.User.Update(userToEdit);
                    //_saleContext.SaveChangesAsync();
                    return userToEdit;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from user, the exception: " + ex.Message, 1);
                throw new Exception("Logging from user, the exception: " + ex.Message);

            }
            return u;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _saleContext.User.ToListAsync();
        }



        //public async Task<List<User>> GetAllUsersAsync()
        //{
        //    return await _saleContext.User.ToListAsync();
        //}

        public string GenerateToken(User user)
        {
            string status = "user";
            if (user.IsAdmin)
                status = "manager";
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Password),
                new Claim(ClaimTypes.NameIdentifier, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.OtherPhone, user.Phone),
                new Claim(ClaimTypes.Role, status)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


