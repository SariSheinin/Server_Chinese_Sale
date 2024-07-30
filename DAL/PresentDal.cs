using Chinese_Sale.BL;
using Chinese_Sale.Migrations;
using Microsoft.EntityFrameworkCore;
using sale_server.Models;
using sun.util.resources.cldr.nl;
using System.Collections;
using System.Xml.Linq;

namespace Chinese_Sale.DAL
{
    public class PresentDal : IPresentDal
    {
        private readonly SaleContext _saleContext;
        ILogger<Present> _logger;
        IPresentsOrderDal PresentsOrderDal;
        public PresentDal(SaleContext saleContext, ILogger<Present> logger, IPresentsOrderDal PresentsOrderDal)
        {
            this._saleContext = saleContext ?? throw new ArgumentNullException(nameof(saleContext));
            this._logger = logger;
            this.PresentsOrderDal = PresentsOrderDal;
        }

        public async Task<int> AddPresentAsync(Present p)
        {

            //var j = _saleContext.Present.Include(p => _saleContext.Donor);
            await _saleContext.Present.AddAsync(p);
            await _saleContext.SaveChangesAsync();
            //להחזיר את הID שנוצר בDB
            return p.Id;


            //catch (Exception ex)
            //{
            //    _logger.LogError("Logging from prese, the exception: " + ex.Message, 1);
            //    throw new Exception("Logging from donor, the exception: " + ex.Message);
            //}
        }

        public async Task<int> DeletePresentAsync(int id)
        {
            try
            {
                _saleContext.Remove(_saleContext.Present.Find(id));
                await _saleContext.SaveChangesAsync();
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }
        }

        public async Task<bool> EditPresentAsync(Present p)
        {
            try
            {
                Present presentToEdit = await _saleContext.Present.FirstOrDefaultAsync(item => item.Id == p.Id);
                if (presentToEdit != null)
                {
                    presentToEdit.Name = p.Name;
                    presentToEdit.DonorId = p.DonorId;
                    presentToEdit.CardPrice = p.CardPrice;
                    presentToEdit.Image = p.Image;

                    _saleContext.Present.Update(presentToEdit);
                    await _saleContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);

            }
            return false;
        }

        public async Task<List<Present>> FilterPresentsByNameAndDonorAsync(string? name, string? donorName)
        {
            try
            {
                var presentsInclude = _saleContext.Present.Include(p => p.Donor);

                var q = presentsInclude.Where(present =>
                (name == null ? (true) : (present.Name.Contains(name)))
                && (donorName == null ? (true) : (present.Donor.Name.Contains(donorName))));
                List<Present> presents = await q.ToListAsync();
                return presents;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }
        }

        public async Task<List<Present>> FilterPresentsByNumOfPurchasersAsync(int? numOfPurchasers)
        {
            var presents = await _saleContext.PresentsOrder
                         .Include(po => po.Present)
                         .Include(po => po.Order)
                         .Where(po => po.Order.IsDraft == false)
                         .GroupBy(po => po.Present.Id)
                         .Where(po => po.Count() == numOfPurchasers)
                         .Select(po => po.First().Present)
                         .ToListAsync();
            return presents;
        }

        public async Task<IEnumerable> GetPresntsWhithDonor()
        {
            var presnts = (from Present present in _saleContext.Present
                           orderby present.Name
                           select new
                           {
                               present.Id,
                               present.Name,
                               present.Image,
                               DonorName = present.Donor.Name
                           }
                          );
            return await presnts.ToListAsync<object>();
        }

        public async Task<Donor> GetDonorAsync(int presentId)
        {


            try
            {
                Present q = await _saleContext.Present.FirstOrDefaultAsync(p => p.Id == presentId);
                Donor q2 = await _saleContext.Donor.FirstOrDefaultAsync(d => d.Id == q.DonorId);
                return q2;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }
        }

        public async Task<List<Present>> GetPresentsListAsync()
        {
            List<Present> presents = await _saleContext.Present.ToListAsync();
            foreach (var p in presents)
            {
                p.Donor = await _saleContext.Donor.FirstOrDefaultAsync(d => d.Id == p.DonorId);
            }
            return presents;
        }

        public async Task<List<Present>> SortByCatgoreAndPriceAsync(Category?[] category, int? minPrice, int? maxPrice)
        {

            //          var alp = _saleContext.Donor.Where(donor =>
            //(name == null ? (true) : (donor.Name.Contains(name)))
            //&& (email == null ? (true) : (donor.Email. (email))))
            //    .Include(d => d.DonationsList.Where(p => p.Name.Contains(present)));

            var a = _saleContext.Present.Where(present =>
               ((minPrice == null) ? (true) : (present.CardPrice >= minPrice))
            && ((maxPrice == null) ? (true) : (present.CardPrice <= maxPrice))
           && (category.Length == 0) ? (true) : (category.Contains(present.Category)));

            return a.ToList();
        }


    }
}
