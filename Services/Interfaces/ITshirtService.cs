using Domain.Entities;
using Domain.RequestFeatures;
using Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITshirtService
    {
        Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email);
        Task<PagedList<TShirt>> GetTShirtsAsync(TShirtParameters tshirtParameters);
        //Task<PagedList<TShirtToReturnDTO>> GetTShirtsAsync(TShirtParameters tshirtParameters);
        Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id);
        Task<PagedList<TShirt>> GetAllByCurrentUserAsync(string email, TShirtParameters tshirtParameters);
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<IEnumerable<GenderDTO>> GetGendersAsync();
        Task<PagedList<TShirt>> GetByUserAsync(string name, TShirtParameters tshirtParameters);
    }
}
