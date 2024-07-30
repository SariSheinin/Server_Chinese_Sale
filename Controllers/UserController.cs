using AutoMapper;
using Chinese_Sale.BL;
using Chinese_Sale.DTOs;
using Chinese_Sale.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User = Chinese_Sale.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chinese_Sale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserRegisterDTO> _logger;
        private readonly IUserService _userService;
        private IConfiguration _config;


        public UserController(IMapper mapper, ILogger<UserRegisterDTO> logger, IUserService userService, IConfiguration _config)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._config = _config;
            this._userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var res = await _userService.GetAllUsers();
                //var _res = _mapper.Map<List<UserRegisterDTO>>(res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet]
        [Route("GetUserById")]
        public async Task<ActionResult<UserRegisterDTO>> GetUserById(int id)
        {
            try
            {
                return Ok(await _userService.GetUserById(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserRegisterDTO>> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                var _user = _mapper.Map<User>(user);
                var createdUser = await _userService.AddUser(_user);

                if (createdUser != null)
                    return Ok(createdUser);
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut]
        [Route("EditUser")]
        public async Task<ActionResult<UserRegisterDTO>> EditUser(UserRegisterDTO user)
        {
            try
            {
                var _user = _mapper.Map<User>(user);
                var createdUser = await _userService.UpdateUserDetails(_user);
                if (createdUser != null)
                    return Ok(createdUser);
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST api/<UsersController>/login
        //[HttpPost("Login")]
        //async public Task<ActionResult> Login([FromBody] UserLoginDTO userDTO)
        //async public Task<int> Login([FromBody] UserLoginDTO userDTO)
        //{

        //    //try
        //    //{
        //        //var user = await _user.Authenticate(userLogin);

        //        //if (user != null)
        //        //{
        //            //object token = _user.Generate(user);
        //        //    var jsonToken = JsonConvert.SerializeObject(new { token });
        //        //    return Ok(new { jsonToken });
        //        //    //return Ok(token);
        //        //}
        //        //return NotFound("User not found");
        //        User user = await _userService.GetUserByUserNameAndPassword(userDTO.UserName, userDTO.Password);
        //        if (user != null)
        //        {
        //            //UserLoginDTO createdUserDTO = _mapper.Map<UserLoginDTO>(user);
        //            object token = _userService.GenerateToken(user);
        //            var jsonToken = JsonConvert.SerializeObject(new { token });
        //            _logger.LogInformation($"Login attempted with User Name {user.UserName} and password {user.Password}");
        //            //return Ok(new { jsonToken });
        //            return user.Id;
        //        }
        //        //else
        //            //return RedirectToAction("Register");
        //            return 134;

        //        //return NoContent();
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    _logger.LogError($"error while login: {ex}");
        //    //    //return BadRequest();
        //    //    return -1;
        //    //}
        //}
        // POST api/<UsersController>/login
        [HttpPost("Login")]
         public async Task<IActionResult> Login([FromBody] UserLoginDTO userDTO)
        {

            try
            {
                User user = await _userService.GetUserByUserNameAndPassword(userDTO.UserName, userDTO.Password);
                if (user != null)
                {
                    var token = Generate(user);
                    var isAdmin = user.IsAdmin;
                    var jsonToken = JsonConvert.SerializeObject(new { token, isAdmin });
                    return Ok(jsonToken);
                    //return Ok(token);
                }
                return Ok();
                //return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while login: {ex}");
                return Ok();
            }
        }
        private string Generate(User user)
        {
            string role;
            if (user.IsAdmin)
                role = "admin";
            else
                role = "customer";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.Role, role),
                 new Claim("userId",user.Id.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };




            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
