using Domain.Entities;
using Domain.RequestFeatures;
using Services.DTOs;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITshirtService
    {
        Task<PagedList<TShirt>> GetAllByCurrentUserAsync(string email, TShirtParameters tshirtParameters);
        Task<PagedList<TShirt>> GetByUserAsync(string name, TShirtParameters tshirtParameters);
        Task<PagedList<TShirt>> GetTShirtsAsync(TShirtParameters tshirtParameters);
        Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id);
        Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email);

        //Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        //Task<IEnumerable<GenderDTO>> GetGendersAsync();
    }
}
