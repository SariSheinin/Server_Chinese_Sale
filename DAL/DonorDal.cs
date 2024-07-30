using AutoMapper;
using Chinese_Sale.Models.DTOs;
//using java.lang;
using Microsoft.EntityFrameworkCore;
using sale_server.Models;
using System.Linq;

namespace Chinese_Sale.DAL
{
    public class DonorDal: IDonorDal
    {
        private readonly SaleContext _saleContext;
       private readonly ILogger<Donor> _logger;
        private readonly IMapper _mapper;
        public DonorDal(SaleContext saleContext, ILogger<Donor> logger, IMapper mapper)
        {
            this._saleContext = saleContext ?? throw new ArgumentNullException(nameof(saleContext));
            this._logger = logger;
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
          
        }

        public async Task<int> AddDonorAsync(Donor d)
        {
            try
            {
               await _saleContext.Donor.AddAsync(d);
               await _saleContext.SaveChangesAsync();
               return d.Id;
            }

            catch(Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }
          
            //await _saleContext.Donor.ToListAsync();
        }

        public async Task<bool> DeleteDonorAsync(int id)
        {
            try
            {
                var d = _saleContext.Donor.Find(id);
                Present p =await _saleContext.Present.FirstOrDefaultAsync(pr => pr.DonorId == id);
                Console.WriteLine(p);
                if (p != null)
                    return false;
                else
                _saleContext.Remove(d);
                await _saleContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            }
        }

        public async Task<bool> EditDonorAsync(Donor d)
        {
            try
            {
                Donor donorToEdit = await _saleContext.Donor.FirstOrDefaultAsync(item => item.Id == d.Id);
                if (donorToEdit != null)
                {
                    donorToEdit.Name = d.Name;
                    donorToEdit.Address = d.Address;
                    donorToEdit.Email = d.Email;
                    //donorToEdit.TypeOfDonation = d.TypeOfDonation;
                    _saleContext.Donor.Update(donorToEdit);
                    _saleContext.SaveChangesAsync();
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

       public async Task<List<Donor>> FilterDonors(string? name, string? email, string? present)
        {
            try
            {
                var p = await _saleContext.Present.FirstOrDefaultAsync(e => e.Name == present);

                var q = _saleContext.Donor.Where(donor =>
                (name == null ? (true) : (donor.Name.Contains(name)))
                && (email == null ? (true) : (donor.Email.Contains(email)))
                && (present == null ? (true) : (donor.Id== p.DonorId)));
               

                return q.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Logging from donor, the exception: " + ex.Message, 1);
                throw new Exception("Logging from donor, the exception: " + ex.Message);
            } 
        }



        public async Task<List<Present>> GetDonationListAsync(int id)
        {
            var presents = await _saleContext.Present.Where(p => p.DonorId == id).ToListAsync();
            return presents;
        }

        public async Task<List<Donor>> GetDonorsListAsync()
        {
            return await _saleContext.Donor.ToListAsync();
        }

    }
}
