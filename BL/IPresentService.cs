using sale_server.Models;

namespace Chinese_Sale.BL
{
    public interface IPresentService
    {
        Task<List<Present>> GetPresentsList();
        Task<int> AddPresent(Present p);
        Task<int> DeletePresent(int id);
        Task<bool> EditPresent(Present p);
        Task<Donor> GetDonor(int presentId);
        public Task<List<Present>> FilterPresentsByNameAndDonor(string? name, string? donorName);
        Task<List<Present>> SortByCatgoreAndPrice(Category?[] category, int? minPrice, int? maxPrice);
        public Task<List<Present>> FilterPresentsByNumOfPurchasers(int? numOfPurchasers);


    }
}
