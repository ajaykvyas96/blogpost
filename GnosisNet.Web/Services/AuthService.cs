using GnosisNet.Web.IServices;
using GnosisNet.Web.Models;
using GnosisNet.Web.Models.Enums;
using GnosisNet.Web.Providers;
using GnosisNet.Web.Utility;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http;

namespace GnosisNet.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthService(IBaseService baseService, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _baseService = baseService;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;

        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            var apiResponse = await _baseService.SendAsync(new RequestDto
            {
                Url = "api/Auth/login",
                ApiType = ApiType.POST,
                Data = loginRequest
            }, false);
            if (apiResponse.IsSuccess)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(apiResponse?.Result.ToString());
                await _localStorage.SetItem(ClientConstantKeys.JwtToken, loginResponse.Token);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResponse.User.Email);
                return loginResponse;
            }
            return new LoginResponseDto();
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItem(ClientConstantKeys.JwtToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }

        public async Task<ResponseDto> Register(RegistrationRequestDto registrationRequest)
        {
            var apiResponse = await _baseService.SendAsync(new RequestDto
            {
                Url = "api/Auth/register",
                ApiType = ApiType.POST,
                Data = registrationRequest
            }, false);
            return apiResponse;
        }
    }
}
