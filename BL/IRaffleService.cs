using Chinese_Sale.Models;
using sale_server.Models;

namespace Chinese_Sale.BL
{
    public interface IRaffleService
    {
        public Task<User> RaffleWinner(int presentId);
        public Task<List<Raffle>> RaffleReport();

    }
}
