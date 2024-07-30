using Chinese_Sale.Models;
using Microsoft.Extensions.Hosting;
using sale_server.Models;

namespace Chinese_Sale.DAL
{
    public interface IOrderDal
    {
        public Task<int> AddCartAsync(int userId);
        public Task<int> DeleteCartAsync(int id);
        public Task<Order> GetCartAsync(int userId);
        public Task<List<User>> GetPurchacersDetailsAsync();
        public Task<int> PayAsync(int orderId);
        public Task<int> GetSumOfCarts();
    }
}
