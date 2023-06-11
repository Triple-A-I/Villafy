using VillaWeb.Models.Dto.Villa;

namespace VillaWeb.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaCreateDto villaCreateDto, string token);
        Task<T> UpdateAsync<T>(VillaUpdateDto villaUpdateDto, string token);
    }
}
