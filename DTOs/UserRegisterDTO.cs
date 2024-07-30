namespace Chinese_Sale.DTOs
{
    public class UserRegisterDTO
    { 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
    }
}
