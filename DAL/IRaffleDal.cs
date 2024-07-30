using Chinese_Sale.Models;
using sale_server.Models;

namespace Chinese_Sale.DAL
{
    public interface IRaffleDal
    {
        public Task<User> RaffleWinnerAsync(int presentId);
        public Task<List<Raffle>> RaffleReportAsync();
    }
}
