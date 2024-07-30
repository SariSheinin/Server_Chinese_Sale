using Chinese_Sale.DAL;
using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using com.sun.org.glassfish.external.statistics.annotations;
using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.AspNetCore.Mvc;
using sale_server.Models;

namespace Chinese_Sale.BL
{
    public class PresentsOrderService : IPresentsOrderService
    {
        private readonly IPresentsOrderDal _PresentsOrderDal;
        private readonly ILogger<PresentsOrder> _logger;

        public PresentsOrderService(IPresentsOrderDal presentsOrderDal, ILogger<PresentsOrder> logger)
        {
            this._PresentsOrderDal = presentsOrderDal;

        }
        public async Task<List<PresentsOrder>> GetCartsByUserId(int userId)
        {
            return await _PresentsOrderDal.GetCartsByUserId(userId);
        }

        public async Task<int> AddPresentToCart(PresentsOrder present)
        {
            return await _PresentsOrderDal.AddPresentToCartAsync(present);
        }

        public  async Task<int> DeletePresentFromCart(int opId)
        {
            return await _PresentsOrderDal.DeletePresentFromCartAsync(opId);
        }

        public async Task<List<PresentsOrder>> GetPresentsOrder()
        {
            return await _PresentsOrderDal.GetPresentsOrderAsync();
        }

        public async Task<List<PresentsOrder>> GetThePurchasesForEachPresent(int presentId)
        {
            return await _PresentsOrderDal.GetThePurchasesForEachPresentAsync(presentId);
        }

        public async Task<List<Present>> SortByTheMostExpensivePresent()
        {
            return await _PresentsOrderDal.SortByTheMostExpensivePresentAsync();
        }

        public  Task<List<Present>> SortByTheMostPurchasedPresent()
        {
            return  _PresentsOrderDal.SortByTheMostPurchasedPresentAsync();
        }
    }

}