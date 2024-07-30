using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using sale_server.Models;

namespace Chinese_Sale.BL
{
    public interface IPresentsOrderService
    {
        public Task<int> AddPresentToCart(PresentsOrder present);
        public Task<int> DeletePresentFromCart(int opId);
        public Task<List<PresentsOrder>> GetPresentsOrder();
        public Task<List<PresentsOrder>> GetThePurchasesForEachPresent(int presentId);
        public Task<List<Present>> SortByTheMostPurchasedPresent();
        public Task<List<Present>> SortByTheMostExpensivePresent();
        public Task<List<PresentsOrder>> GetCartsByUserId(int userId);

    }
}
