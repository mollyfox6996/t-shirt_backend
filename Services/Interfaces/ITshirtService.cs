using Domain.Entities;
using Domain.RequestFeatures;
using System.Threading.Tasks;
using Services.DTOs.OperationResultDTOs;
using Services.DTOs.TshirtDTOs;

namespace Services.Interfaces
{
    public interface ITshirtService
    {
        Task<PagedList<TShirt>> GetTShirtsAsync(TshirtParameters tshirtParameters);
        Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id);
        Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email);
        Task DeleteAsync(int id);
    }
}
