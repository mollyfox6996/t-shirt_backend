using Domain.Entities;
using Domain.RequestFeatures;
using Services.DTOs;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITshirtService
    {
        Task<PagedList<TShirt>> GetTShirtsAsync(TshirtParameters tshirtParameters);
        Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id);
        Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email);
    }
}
