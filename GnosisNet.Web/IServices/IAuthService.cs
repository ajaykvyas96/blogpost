using GnosisNet.Web.Models;

namespace GnosisNet.Web.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto login);
        Task Logout();
    }
}
