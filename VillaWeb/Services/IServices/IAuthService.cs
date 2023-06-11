using VillaWeb.Models.Dto.Auth;

namespace VillaWeb.Services.IServices
{
    public interface IAuthService
    {
        public Task<T> LoginAsync<T>(LoginRequestDto loginRequestDto);
        public Task<T> RegisterAsync<T>(RegistrationRequestDto registrationRequestDto);
    }
}
