using Chinese_Sale.Models;
using sale_server.Models;

namespace Chinese_Sale.BL
{
    public interface IOrderService
    {
        public Task<int> AddCart(int userId);
        public Task<int> DeleteCart(int id);
        public Task<Order> GetCart(int userId);
        public Task<List<User>> GetPurchacersDetails();
        public Task<int> Pay(int orderId);
        public Task<int> GetSumOfCarts();



    }
}
