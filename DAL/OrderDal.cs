using Chinese_Sale.Models;
using sale_server.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using javax.xml.transform;
using com.sun.xml.@internal.bind.v2.model.core;
using static com.sun.tools.@internal.xjc.reader.xmlschema.bindinfo.BIConversion;
using UserRegister = Chinese_Sale.Models.User;

namespace Chinese_Sale.DAL
{
    public class OrderDal : IOrderDal
    {
        private readonly SaleContext _saleContext;
        private readonly ILogger<Order> _logger;

        public OrderDal(SaleContext saleContext, ILogger<Order> logger)
        {
            this._saleContext = saleContext;
            this._logger = logger;
        }

        public async Task<int> AddCartAsync(int userId)
        {
            try
            {
                var order = await _saleContext.Order.FirstOrDefaultAsync(o => o.UserId == userId);
                if(order == null)
                {
                    Order o = new Order();
                    o.UserId = userId;
                    o.Sum = 0;
                    o.OrderDate = DateTime.Now;
                    o.IsDraft = true;
                    await _saleContext.Order.AddAsync(o);
                    await _saleContext.SaveChangesAsync();
                    return o.Id;
                }
                return order.Id;
            }

            catch (Exception ex)
            {
                _logger.LogError("Logging from order, the exception: " + ex.Message, 1);
                throw new Exception("Logging from order, the exception: " + ex.Message);
            }
        }

        public async Task<int> DeleteCartAsync(int userId)
        {
            try
            {
                var order = await _saleContext.Order.FirstOrDefaultAsync(o => o.UserId == userId);
                _saleContext.Remove(order);
                await _saleContext.SaveChangesAsync();
                return order.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from order, the exception: " + ex.Message, 1);
                throw new Exception("Logging from order, the exception: " + ex.Message);
            }
        }

        public async Task<Order> GetCartAsync(int userId)
        {
            //var query = _saleContext.Order.Include(o => _saleContext.PresentsOrder.Where(po => po.OrderId == o.Id));
            var q = await _saleContext.Order.FirstOrDefaultAsync(o => o.UserId == userId);
            return q;
        }

        public async Task<List<UserRegister>> GetPurchacersDetailsAsync()
        {
            var purchasersInclude = _saleContext.Order.Include(o => o.User);
            var q = purchasersInclude.Where(o => o.IsDraft == false).Select(o => o.User);
            return q.ToList();
        }

        public async Task<int> GetSumOfCarts()
        {
            var c = await _saleContext.Order.Where(o => o.IsDraft == true).SumAsync(c => c.Sum);
            return c;
        }

        public async Task<int> PayAsync(int orderId)
        {
            {
                try
                {
                    var order = await _saleContext.Order.FirstOrDefaultAsync(o => o.Id == orderId);
                    if (order != null)
                    {
                        order.IsDraft = true;
                        _saleContext.Order.Update(order);
                        var q = await _saleContext.PresentsOrder.Where(po => po.OrderId == order.Id&&po.IsDraft==false ).ToListAsync();
                        for (var i = 0; i < q.Count(); i++)
                        {
                            q[i].IsDraft = true;
                            _saleContext.PresentsOrder.Update(q[i]);
                        }
                        int sum = order.Sum;
                        order.Sum = 0;
                        _saleContext.Order.Update(order);

                        _saleContext.SaveChangesAsync();
                 
                        return sum;
                    }
                    return -1;
                }

                catch (Exception ex)
                {
                    _logger.LogError("Logging from order, the exception: " + ex.Message, 1);
                    throw new Exception("Logging from order, the exception: " + ex.Message);
                }
            }
        }
    }
       
   
       
   
}
