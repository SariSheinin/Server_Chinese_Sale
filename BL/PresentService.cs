using Chinese_Sale.DAL;
using sale_server.Models;

namespace Chinese_Sale.BL
{
    public class PresentService : IPresentService
    {
        private readonly IPresentDal _presentDal;
        public PresentService(IPresentDal presentDal)
        {
            this._presentDal = presentDal;   
        }

        public async Task<int> AddPresent(Present p)
        {
            return await _presentDal.AddPresentAsync(p);
        }

        public async Task<int> DeletePresent(int id)
        {
            return await _presentDal.DeletePresentAsync(id);        
        }

        public async Task<bool> EditPresent(Present p)
        {
            return await _presentDal.EditPresentAsync(p);
        }

     
        public async Task<List<Present>> FilterPresentsByNameAndDonor(string? name, string? donorName)
        {
            return await _presentDal.FilterPresentsByNameAndDonorAsync(name, donorName);
        }

        public async Task<List<Present>> FilterPresentsByNumOfPurchasers(int? numOfPurchasers)
        {
            return await _presentDal.FilterPresentsByNumOfPurchasersAsync(numOfPurchasers);
        }

        public async Task<Donor> GetDonor( int presentId)
        {
            return await _presentDal.GetDonorAsync(presentId);
        }

        public async Task<List<Present>> GetPresentsList()
        {
            return await _presentDal.GetPresentsListAsync();
        }

        public async Task<List<Present>> SortByCatgoreAndPrice(Category?[] category, int? minPrice, int? maxPrice)
        {
            return await _presentDal.SortByCatgoreAndPriceAsync(category, minPrice, maxPrice);
        }
    }
}
