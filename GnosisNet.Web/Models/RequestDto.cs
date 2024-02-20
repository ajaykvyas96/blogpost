using GnosisNet.Web.Models.Enums;
using System.Net.Mime;
using System.Security.AccessControl;
using ContentType = GnosisNet.Web.Models.Enums.ContentType;

namespace GnosisNet.Web.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
