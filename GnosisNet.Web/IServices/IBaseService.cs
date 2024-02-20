using GnosisNet.Web.Models;

namespace GnosisNet.Web.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
