using Chinese_Sale.Models;
using Microsoft.EntityFrameworkCore;
using sale_server.Models;
using System.Linq;

namespace Chinese_Sale.DAL
{
    public class RaffleDal : IRaffleDal
    {
        //private readonly IRaffleDal _raffleDal;
        private readonly IPresentsOrderDal _presentsOrderDal;
        private readonly SaleContext _saleContext;

        public RaffleDal(IPresentsOrderDal presentsOrderDal, SaleContext saleContext) { 
         
            //this._raffleDal = raffleDal;
            this._presentsOrderDal = presentsOrderDal;
            this._saleContext = saleContext ?? throw new ArgumentNullException(nameof(saleContext));

        }

        public async Task<List<Raffle>> RaffleReportAsync()
        {

            return await _saleContext.Raffle
                .Include(r => r.present)
                .Include(r => r.user)
                .Select(r => new Raffle() { Id = r.Id, PresentId = r.PresentId, user = new User() { FullName = r.user.FullName, Email = r.user.Email, Phone = r.user.Phone, }, UserId = r.UserId, present = r.present })
                .ToListAsync();
        }

        public async Task<User> RaffleWinnerAsync(int presentId)
        {
            List<PresentsOrder> allPurchasersForThisPresent = new List<PresentsOrder>();

            var thereIsAlreadyWinner = await _saleContext.Raffle.FirstOrDefaultAsync(r => r.PresentId == presentId);
            if (thereIsAlreadyWinner != null)
                return null;
            var purchasersForThisPresent = await _saleContext.PresentsOrder.Where(po => po.PresentId == presentId).ToListAsync();
            purchasersForThisPresent.ForEach(item =>
            {
                if (item != null)
                {
                    if (item.Quantity > 0)
                    {
                        for (var i = 0; i < item.Quantity; i++)
                            allPurchasersForThisPresent.Add(item);
                    }
                    else
                        allPurchasersForThisPresent = purchasersForThisPresent;
                }
            }
            );
            if(allPurchasersForThisPresent.Count != 0)
            {
                Random r = new Random();
                int index = r.Next(allPurchasersForThisPresent.Count);
                var orderId = allPurchasersForThisPresent[index].OrderId;
                var winnerOrder = await _saleContext.Order.FirstOrDefaultAsync(o => o.Id == orderId);
                var user = await _saleContext.User.FirstOrDefaultAsync(u => u.Id == winnerOrder.UserId);
                await _saleContext.Raffle.AddAsync(new Raffle() { PresentId = presentId, UserId = user.Id });

                await _saleContext.SaveChangesAsync();
                return user;
            }
            return null;
        }
     }
}

