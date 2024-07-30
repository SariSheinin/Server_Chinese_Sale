using sale_server.Models;

namespace Chinese_Sale.DAL
{
    public interface IPresentDal
    {
        Task<List<Present>> GetPresentsListAsync();
        Task<int> AddPresentAsync(Present p);
        Task<int> DeletePresentAsync(int id);
        Task<bool> EditPresentAsync(Present p);
        Task<Donor> GetDonorAsync(int presentId);
        public Task<List<Present>> FilterPresentsByNameAndDonorAsync(string? name, string? donorName);
        Task<List<Present>> SortByCatgoreAndPriceAsync(Category?[] category, int? minPrice, int? maxPrice);
        public Task<List<Present>> FilterPresentsByNumOfPurchasersAsync(int? numOfPurchasers);


    }
}
