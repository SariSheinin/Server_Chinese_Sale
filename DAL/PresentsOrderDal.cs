using AutoMapper;
using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using com.sun.org.glassfish.external.statistics.annotations;
using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sale_server.Models;
using sun.util.resources.cldr.nl;

namespace Chinese_Sale.DAL
{
    public class PresentsOrderDal: IPresentsOrderDal
    {
        private readonly SaleContext _saleContext;
        private readonly ILogger<PresentsOrder> _logger;
        private readonly IMapper _mapper;

        public PresentsOrderDal(SaleContext saleContext, ILogger<PresentsOrder> logger, IMapper mapper)
        {
            this._saleContext = saleContext;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<int> AddPresentToCartAsync(PresentsOrder present)
        {
            try
            {

                Order order = await _saleContext.Order.FirstOrDefaultAsync(o => o.Id == present.OrderId);
                Present p = await _saleContext.Present.FirstOrDefaultAsync(p => p.Id == present.PresentId);

                order.Sum = order.Sum + p.CardPrice;

                PresentsOrder item = await _saleContext.PresentsOrder.FirstOrDefaultAsync(o => o.OrderId == present.OrderId && o.PresentId == present.PresentId&&o.IsDraft==false);
                if (item!=null)
                {
                    item.IsDraft = false;
                    item.Quantity++;
                    await _saleContext.SaveChangesAsync();
                }
                else
                {
                    await _saleContext.PresentsOrder.AddAsync(present);
                    await _saleContext.SaveChangesAsync();
                }
                return present.Id;  
            }

            catch (Exception ex)
            {
                _logger.LogError("Logging from cart, the exception: " + ex.Message, 1);
                throw new Exception("Logging from cart, the exception: " + ex.Message);
            }

        }

        public async Task<List<PresentsOrder>> GetCartsByUserId(int userId)
        {
             
                List<PresentsOrder>presents =await _saleContext.PresentsOrder.Where(i=>i.OrderId==userId&&i.IsDraft==false).ToListAsync();
            foreach (var item in presents)
            {
                item.Present= await _saleContext.Present.Where(i => i.Id == item.PresentId).FirstOrDefaultAsync();

            }
            return presents;
        }

            public async Task<int> DeletePresentFromCartAsync(int opId)
            {
            try
            {
                PresentsOrder po = await _saleContext.PresentsOrder.FirstOrDefaultAsync(po => po.Id == opId);
                if (po != null)
                {
                    if (po.IsDraft == false)
                    {
                        var order = await _saleContext.Order.FirstOrDefaultAsync(o => o.Id == po.OrderId);
                        var p = await _saleContext.Present.FirstOrDefaultAsync(p => p.Id == po.PresentId);
                        order.Sum = order.Sum - p.CardPrice;

                        PresentsOrder item = await _saleContext.PresentsOrder.FirstOrDefaultAsync(o => o.Id == po.Id && o.PresentId == po.PresentId);
                        if (item != null)
                        {
                            if (item.Quantity == 1)
                                _saleContext.PresentsOrder.Remove(po);
                            else
                                item.Quantity--;
                            await _saleContext.SaveChangesAsync();
                            return opId;

                        }
                        else
                            return -1;
                    }
                    else
                        return -1;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from presentsOrder, the exception: " + ex.Message, 1);
                throw new Exception("Logging from presentsOrder, the exception: " + ex.Message);
            }
        }

        public async Task<List<PresentsOrder>> GetPresentsOrderAsync()
        {
            //return await _saleContext.PresentsOrder.Where(po=> po.IsDraft == true).ToListAsync();
            return await _saleContext.PresentsOrder.ToListAsync();
        }

        public async Task<List<PresentsOrder>> GetThePurchasesForEachPresentAsync(int presentId)
        {
            var presents =  _saleContext.PresentsOrder.Where(po => po.PresentId == presentId);
            return presents.ToList();
        }

        public Task<List<Present>> SortByTheMostExpensivePresentAsync()
        {
            var q = _saleContext.Present.OrderByDescending(p => p.CardPrice);
            return q.ToListAsync();
        }

        public async Task<List<Present>> SortByTheMostPurchasedPresentAsync()
        {
            //var allPresentsOrder = await _saleContext.PresentsOrder.Where(po=> po.IsDraft == false).ToListAsync();
            //allPresentsOrder.ForEach(item =>
            //{
            //    if (item != null)
            //    {
            //        if (item.Quantity > 0)
            //        {
            //            for (var i = 0; i < item.Quantity; i++)
            //                allPresentsOrder.Add(item);
            //        }
            //        else
            //            allPresentsOrder = allPresentsOrder;
            //    }
            //});
            var presents = await _saleContext.PresentsOrder
                          .Include(po => po.Present)
                          .Include(po => po.Order)
                          .Where(po => po.Order.IsDraft == false)
                          .GroupBy(po => po.Present.Id)
                          .OrderByDescending(po => po.Count())
                          .Select(po => po.First().Present)
                          .ToListAsync();
            return presents;
        }
        
    }
}
