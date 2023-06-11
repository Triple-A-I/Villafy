using VillaWeb.Models.Dto.VillaNumber;

namespace VillaWeb.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNumberCreateDto villaNumberCreateDto, string token);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDto villaNumberUpdateDto, string token);
    }
}
