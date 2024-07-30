using Chinese_Sale.DAL;
using Chinese_Sale.Models;
using sale_server.Models;

namespace Chinese_Sale.BL
{
    public class OrderService : IOrderService
    {
       
       
            private readonly IOrderDal _orderDal;
            private readonly ILogger<Order> _logger;

            public OrderService(IOrderDal orderDal, ILogger<Order> logger)
            {
                this._orderDal = orderDal;
                this._logger = logger;
            }
            public async Task<int> AddCart(int userId)
            {
                return await _orderDal.AddCartAsync(userId);
            }

            public async Task<int> DeleteCart(int id)
            {
                return await _orderDal.DeleteCartAsync(id);
            }

            public async Task<Order> GetCart(int userId)
            {
                return await _orderDal.GetCartAsync(userId);
            }

            public async Task<List<User>> GetPurchacersDetails()
            {
            return await _orderDal.GetPurchacersDetailsAsync();
            }

      

            public Task<int> Pay(int orderId)
            {
            return _orderDal.PayAsync(orderId);
            }
            public  async Task<int> GetSumOfCarts()
            {
            return await _orderDal.GetSumOfCarts();
            }
    }
}


