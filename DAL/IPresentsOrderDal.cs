using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using sale_server.Models;

namespace Chinese_Sale.DAL
{
    public interface IPresentsOrderDal
    {
        public Task<int> AddPresentToCartAsync(PresentsOrder present);
        //Task<int> AddToPresentCartAsync(Order o);
        public Task<int> DeletePresentFromCartAsync(int opId);
        public Task<List<PresentsOrder>> GetPresentsOrderAsync();
        public Task<List<PresentsOrder>>GetThePurchasesForEachPresentAsync(int presentId);
        public Task<List<Present>> SortByTheMostPurchasedPresentAsync();
        public Task<List<Present>> SortByTheMostExpensivePresentAsync();
        public Task<List<PresentsOrder>> GetCartsByUserId(int userId);
    }
}
