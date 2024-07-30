using Chinese_Sale.DAL;
using Chinese_Sale.Models.DTOs;
using sale_server.Models;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Chinese_Sale.BL
{
    public class DonorService : IDonorService
    {
        private readonly IDonorDal _donorDal;

        public DonorService(IDonorDal donorDal)
        {
            this._donorDal = donorDal;
        }

        public async Task<int> AddDonor(Donor d)
        {
            return await _donorDal.AddDonorAsync(d);
        }

        public async Task<bool> DeleteDonor(int id)
        {
            return await _donorDal.DeleteDonorAsync(id);
        }

        public async Task<bool> EditDonor(Donor d)
        {
            return await _donorDal.EditDonorAsync(d);
        }

        public  async Task<List<Donor>> FilterDonors(string? name, string? email, string? present)
        {
            return await _donorDal.FilterDonors(name, email, present);
        }

        public async Task<List<Present>> GetDonationList(int id)
        {
            return await _donorDal.GetDonationListAsync(id);
        }

        public async Task<List<Donor>> GetDonorsList()
        {
            return await _donorDal.GetDonorsListAsync();
        }

        //public async Task<List<Donor>> FilterByName(string name)
        //{
        //    return await _donorDal.FilterByNameAsync(name);
        //}
        //public async Task<List<Donor>> FilterByEmail(string email)
        //{
        //    return await _donorDal.FilterByEmailAsync(email);
        //}
        //public async Task<List<Donor>> FilterByPresent(string present)
        //{
        //    return await _donorDal.FilterByNameAsync(present);
        //}



    }
}
