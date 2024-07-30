using sale_server.Models;

namespace Chinese_Sale.Models
{
    public class Raffle
    {
        public int Id { get; set; }
        public DateTime RaffleDate { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public int PresentId { get; set; }
        public Present present { get; set; }
    }
}
