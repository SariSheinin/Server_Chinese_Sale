using Chinese_Sale.Models.DTOs;
using sale_server.Models;

namespace Chinese_Sale.DAL
{
    public interface IDonorDal
    {
        Task<List<Donor>> GetDonorsListAsync();

        Task<int> AddDonorAsync(Donor d);

        Task<bool> DeleteDonorAsync(int id);
        Task<bool> EditDonorAsync(Donor d);
        Task<List<Donor>> FilterDonors(string name, string email, string present);
        Task<List<Present>> GetDonationListAsync(int id);
    }
}
