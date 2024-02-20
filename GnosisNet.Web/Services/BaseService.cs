using GnosisNet.Web.IServices;
using GnosisNet.Web.Models;
using GnosisNet.Web.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace GnosisNet.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public BaseService(HttpClient httpClient, ILocalStorageService localStorage)
        {

            _httpClient = httpClient;
            _localStorage = localStorage;

        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                var request = new HttpRequestMessage();
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        request.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        request.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        request.Method = HttpMethod.Put;
                        break;
                    default:
                        request.Method = HttpMethod.Get;
                        break;
                }
                request.RequestUri = new Uri(_httpClient.BaseAddress,requestDto.Url);
                if (withBearer)
                {
                    var token = await _localStorage.GetToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        request.Headers.Add("Authorization", $"Bearer {token}");
                    }
                }
                if (requestDto.Data != null)
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                var response = await _httpClient.SendAsync(request);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await response.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
