using Chinese_Sale.DAL;
using Chinese_Sale.Models;
using sale_server.Models;

namespace Chinese_Sale.BL
{
    public class RaffleService : IRaffleService
    {
        private readonly IRaffleDal _raffleDal;
        public RaffleService(IRaffleDal raffleDal) 
        {
            this._raffleDal = raffleDal;
        }

        public async Task<List<Raffle>> RaffleReport()
        {
            return await _raffleDal.RaffleReportAsync();
        }

        public async Task<User> RaffleWinner(int presentId)
        {
            return await _raffleDal.RaffleWinnerAsync(presentId);
        }
    }
}
