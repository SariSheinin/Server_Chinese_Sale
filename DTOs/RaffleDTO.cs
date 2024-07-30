using sale_server.Models;

namespace Chinese_Sale.Models.DTOs
{
    public class RaffleDTO
    {
        public int UserId { get; set; }
        public User user { get; set; }
        public int PresentId { get; set; }
        public Present present { get; set; }
    }
}
