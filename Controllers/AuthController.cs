using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chinese_Sale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration configuration;
        public AuthController(IConfiguration configuration)//, UserManager<IdentityUser> userManager)
        {
            this.configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] UserLoginDTO user)
        {
            IActionResult response = Unauthorized();

            if (user != null)
            {
                if (user.UserName.Equals("test@gmail.com") && user.Password.Equals("a"))
                {
                    var issuer = configuration["Jwt:Issuer"];
                    var audience = configuration["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature
                    );

                    var subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                    });

                    var expires = DateTime.UtcNow.AddMinutes(10);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = expires,
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = signingCredentials
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    return Ok(jwtToken);
                }
            }
            return response;
        }
        //    [HttpGet]
        //    [Route("GetUser")]
        //    public async User GetUser()
        //    {
        //        //var name = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "sub")?.Value;

        //        //var user = await _userManager.FindByEmailAsync(name);

        //        // You can also just take part after return and use it in async methods.
        //        private async Task<User> GetCurrentUser()
        //        {
        //            return await _manager.GetUserAsync(HttpContext.User);
        //        }

        //        // Generic demo method.
        //        public async Task DemoMethod()
        //        {
        //            var user = await GetCurrentUser();
        //            string userEmail = user.Email; // Here you gets user email 
        //            string userId = user.Id;
        //        }
        //    }

        //    public CompetitionsController(UserManager<IdentityUser> userManager)
        //    {
        //        _userManager = userManager;
        //    }

        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //}
    }
}
