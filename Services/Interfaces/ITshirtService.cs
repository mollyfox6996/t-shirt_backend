using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITshirtService
    {
        Task<OperationResultDTO<string>> CreateAsync(CreateTshirtDTO model, string email);
        Task<IEnumerable<TShirtToReturnDTO>> GetAllAsync();
        Task<OperationResultDTO<TShirtToReturnDTO>> GetByIdAsync(int id);
        Task<IEnumerable<TShirtToReturnDTO>> GetAllByCurrentUserAsync(string email);
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<IEnumerable<TShirtToReturnDTO>> GetByUserAsync(string name);
    }
}
