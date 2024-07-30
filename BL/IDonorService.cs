using Chinese_Sale.Models.DTOs;
using sale_server.Models;

namespace Chinese_Sale.BL
{
    public interface IDonorService
    {
        Task<List<Donor>> GetDonorsList();
        Task<int> AddDonor(Donor d);
        Task<bool> DeleteDonor(int id);
        Task<bool> EditDonor(Donor d);
        Task<List<Donor>> FilterDonors(string name, string email, string present);
        Task<List<Present>> GetDonationList(int id);




    }
}
