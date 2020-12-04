using Domain.Entities;
using Domain.RequestFeatures;
using Services.DTOs;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> refs/remotes/origin/dev
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITshirtService
    {
<<<<<<< HEAD
        Task<PagedList<TShirt>> GetAllByCurrentUserAsync(string email, TShirtParameters tshirtParameters);
        Task<PagedList<TShirt>> GetByUserAsync(string name, TShirtParameters tshirtParameters);
        Task<PagedList<TShirt>> GetTShirtsAsync(TShirtParameters tshirtParameters);
        Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id);
        Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email);

        //Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        //Task<IEnumerable<GenderDTO>> GetGendersAsync();
=======
        Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email);
        Task<PagedList<TShirt>> GetTShirtsAsync(TShirtParameters tshirtParameters);
        //Task<PagedList<TShirtToReturnDTO>> GetTShirtsAsync(TShirtParameters tshirtParameters);
        Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id);
        Task<PagedList<TShirt>> GetAllByCurrentUserAsync(string email, TShirtParameters tshirtParameters);
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<IEnumerable<GenderDTO>> GetGendersAsync();
        Task<PagedList<TShirt>> GetByUserAsync(string name, TShirtParameters tshirtParameters);
>>>>>>> refs/remotes/origin/dev
    }
}
