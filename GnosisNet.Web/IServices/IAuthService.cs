using GnosisNet.Web.Models;

namespace GnosisNet.Web.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto<LoginResponseDto>> Login(LoginRequestDto login);
        Task Logout();
    }
}
